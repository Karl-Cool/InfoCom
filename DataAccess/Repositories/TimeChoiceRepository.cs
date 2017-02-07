using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using NHibernate.Linq;
using System.Diagnostics;
using NHibernate;

namespace DataAccess.Repositories
{
    public static class TimeChoiceRepository
    {
        public static TimeChoice Get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var timeChoice = session.Query<TimeChoice>().FirstOrDefault(x => x.Id == id);
                    return timeChoice;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;
        }

        public static List<TimeChoice> Get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    List<TimeChoice> timeChoiceList = session.Query<TimeChoice>()
                        .Where(x => x.User.Inactive == false)
                        .ToList();
                    return timeChoiceList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;
        }

        public static int GetCount(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    int count = session.Query<TimeChoice>().Where(x => x.Time.Id == id).ToList().Count;
                    return count;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return 0;
        }

        public static bool Delete(int id)
        {
            var response = false;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var time = session.Query<TimeChoice>().FirstOrDefault(x => x.Id == id);
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

        public static int Add(TimeChoice timeChoice)
        {
            int id = 0;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    id = Convert.ToInt32(session.Save(timeChoice));
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
