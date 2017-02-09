using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataAccess.Models;
using NHibernate.Linq;

namespace DataAccess.Repositories
{
    public class CommentRepository
    {
        public static ICollection<Comment> Get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var comments = session.Query<Comment>()
                        .Where(x => x.Inactive == false && x.Id == id)
                        .ToList();
                    return comments;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;

        }
    }
}
