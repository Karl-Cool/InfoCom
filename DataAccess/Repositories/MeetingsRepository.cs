using System;
using System.Linq;
using DataAccess.Models;
using NHibernate.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public static class MeetingsRepository
    {
        public static List<Meeting> Get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var meetings = session.Query<Meeting>().Where(x => x.Creator.Id == id && x.Inactive == false).ToList();
                    return meetings;
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