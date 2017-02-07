using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using NHibernate.Linq;
using System.Diagnostics;

namespace DataAccess.Repositories
{
    public static class MeetingRepository
    {
        public static Meeting Get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var meeting = session.Query<Meeting>()
                        .Fetch(x => x.Creator)
                        .Fetch(x => x.Times)
                        .Single(x => x.Id == id);
                    return meeting;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public static List<Meeting> Get()
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    List<Meeting> meetingList = session.Query<Meeting>()
                        .Fetch(x => x.Times)
                        .Fetch(x => x.TimeChoices)
                        .Fetch(x => x.Creator).ToList();
                    return meetingList;
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
                    var meeting = session.Query<Meeting>().FirstOrDefault(x => x.Id == id);
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(meeting);
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

        public static int Add(Meeting meeting)
        {
            var id = 0;

            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    id = Convert.ToInt32(session.Save(meeting));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return id;
        }
        public static bool Update(Meeting meeting)
        {
            var success = false;

            try
            {

                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var mtn = session.Query<Meeting>().FirstOrDefault(x => x.Id == meeting.Id);
                    using (var transaction = session.BeginTransaction())
                    {
                        if (mtn != null)
                        {
                            mtn.ConfirmedTime = meeting.ConfirmedTime;
                            mtn.Creator = meeting.Creator;
                            mtn.Description = meeting.Description;
                            mtn.Inactive = meeting.Inactive;
                            mtn.Invitations = meeting.Invitations;
                            mtn.TimeChoices = meeting.TimeChoices;
                            mtn.Times = meeting.Times;
                            mtn.Title = meeting.Title;
                            session.Update(mtn);
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
        public static ICollection<Time> GetTime(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var timechoice = session.Query<TimeChoice>()
                            .Where(x => x.Meeting.Id == id)
                            .GroupBy(x => x.Time)
                            .OrderByDescending(x => x.Count());
                    var time = timechoice.Take(1).Select(x => x.Key).ToList();
                            
                    return time;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
