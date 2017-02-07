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
                        .Where(x => x.Author.Inactive == false)
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
        public static List<Post> GetCat(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var post = session.Query<Post>()
                        .Where(x => x.Category.Id == id)
                        .Fetch(x => x.Author)
                        
                        .OrderByDescending(x => x.CreatedAt).ToList();
                    return post;
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
                    var post = session.Query<Post>().FirstOrDefault(x => x.Author.Id == id);
                    return post;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);

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
