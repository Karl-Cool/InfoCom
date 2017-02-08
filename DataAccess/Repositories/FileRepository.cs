using DataAccess.Models;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DataAccess.Repositories
{
    public static class FileRepository
    {
        public static List<PostFile> Get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {

                    List<PostFile> postList = session.Query<PostFile>().ToList();

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

        public static List<PostFile> Get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    List<PostFile> listOfPostFile = session.Query<PostFile>().Where(x => x.Post.Id == id)
                        .ToList();

                    return listOfPostFile;
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
                    var user = session.Query<PostFile>().FirstOrDefault(x => x.Id == id);
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

        public static bool Add(PostFile fil)
        {
            var response = false;


            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    session.Save(fil);
                    response = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return response;
        }
        public static bool AddThree(List<PostFile> fil)
        {
            var response = false;


            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    foreach(var file1 in fil)
                    {
                        session.Save(file1);
                    }
                    
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