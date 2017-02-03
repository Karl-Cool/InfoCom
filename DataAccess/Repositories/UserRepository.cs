using DataAccess.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccess.Repositories
{
    public static class UserRepository
    {
        public static ICollection<User> Get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var users = session.Query<User>().ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;

        }

        public static User Get(int id)
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

        public static bool Delete(int id)
        {
            var response = false;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var user = session.Query<User>().FirstOrDefault(x => x.Id == id);
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(user);
                        transaction.Commit();
                        response = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return response;

        }

        public static bool Add(User user)
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

        public static bool Update(User user)
        {
            var success = false;

            try
            {

                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var usr = session.Query<User>().FirstOrDefault(x => x.Id == user.Id);
                    using (var transaction = session.BeginTransaction())
                    {
                        if (usr != null)
                        {
                            usr.Email = user.Email;
                            usr.Name = user.Name;
                            usr.Username = user.Username;

                            session.Update(usr);
                        }
                        transaction.Commit();
                    }
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return success;
        }
    }
}

