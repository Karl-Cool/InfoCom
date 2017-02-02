using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using NHibernate.Linq;
using System.Diagnostics;
using NHibernate;

namespace DataAccess.Repositories
{
    public static class TimeRepository
    {
        public static Time get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var time = session.Query<Time>().FirstOrDefault(x => x.Id == id);
                    return time;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;
        }

        public static List<Time> get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    List<Time> timeList = session.Query<Time>().ToList();
                    return timeList;
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
                    var time = session.Query<Time>().FirstOrDefault(x => x.Id == id);
                    ITransaction transaction = session.BeginTransaction();
                    session.Delete(time);
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

        public static int add(Time time)
        {
            int id = 0;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    id = Convert.ToInt32(session.Save(time));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return id;
        }
    }
}
