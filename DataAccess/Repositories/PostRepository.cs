using DataAccess.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public static class PostRepository
    {
        public static List<Post> get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {

                   var postList = session.Query<Post>().Fetch(x => x.Author).ToList();
                    
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
        public static Post get(int id)
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
        public static bool add(Post post)
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
