using DataAccess.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccess.Repositories
{
    public static class PostRepository
    {
        public static List<Post> Get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {

                    var postList = session.Query<Post>()
                        .Where(x => x.Author.Inactive == false && x.Inactive == false)
                        .Fetch(x => x.Author)
                        .Fetch(x => x.Files)
                        .OrderByDescending(x => x.CreatedAt)
                        .ToList();

                    return postList;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);

            }
            return null;
        }


        public static List<Post> GetCat(int id, string formal, bool showHidden)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var post = session.Query<Post>();
                    if (id != 0)
                    {
                        post = post.Where(x => x.Category.Id == id);
                    }
                        

                    if (formal == "Formal")
                    {
                        post = post.Where(x => x.Formal);
                    }
                    else if (formal == "Informal")
                    {
                        post = post.Where(x => x.Formal == false);
                    }

                    if (!showHidden)
                    {
                        post = post.Where(x => x.Inactive == false);
                    }

                    return post.Fetch(x => x.Author).Fetch(x => x.Files).OrderByDescending(x => x.CreatedAt).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);

            }
            return null;
        }
        public static Post Get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var post = session.Query<Post>()
                        .Fetch(x => x.Author)
                        .Fetch(x => x.Files)
                        .FetchMany(x => x.Comments)
                        .ThenFetch(x => x.Author)
                        .Single(x => x.Id == id);

                    return post;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;
        }
        public static bool Add(Post post)
        {
            var response = false;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    session.Save(post);
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return response;
        }
    }
}
