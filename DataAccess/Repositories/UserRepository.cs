using DataAccess.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public static class UserRepository
    {
        public static User get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var user = session.Query<User>().FirstOrDefault(x => x.Id == id);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
            }
            return null;
        }

        public static bool delete(int id)
        {
            var response = false;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var user = session.Query<User>().FirstOrDefault(x => x.Id == id);
                    ITransaction transaction = session.BeginTransaction();
                    session.Delete(user);
                    transaction.Commit();
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return response;

        }

        public static bool add(User user)
        {
            var response = false;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    session.Save(user);
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

