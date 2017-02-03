using System;
using System.Linq;
using DataAccess.Models;
using NHibernate.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using NHibernate;

namespace DataAccess.Repositories
{
    public static class MeetingsRepository
    {
        public static List<Meeting> getMeetingsByUserId(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var meetings = session.Query<Meeting>().Where(x => x.Creator.Id == id).ToList();
                    return meetings;
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
                    var meeting = session.Query<Meeting>().FirstOrDefault(x => x.Id == id);
                    ITransaction transaction = session.BeginTransaction();
                    session.Delete(meeting);
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
    }
}