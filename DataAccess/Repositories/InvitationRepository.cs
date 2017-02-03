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
    public class InvitationRepository
    {
        public static List<Invitation> get(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var invitations = session.Query<Invitation>()
                        .Where(x => x.User.Id == id)
                        .Fetch(x => x.Meeting)
                        .Fetch(x => x.User)
                        .ToList();
                    return invitations;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return null;
        }

        public static void updateNotified(int id)
        {
            try
            {
                using (var session = DbConnect.SessionFactory.OpenSession())
                {
                    var invitations = session.Query<Invitation>()
                        .Where(x => x.User.Id == id && x.Notified == false)
                        .Fetch(x => x.Meeting)
                        .Fetch(x => x.User)
                        .ToList();
                    ITransaction transaction = session.BeginTransaction();
                    invitations.ForEach(x =>
                    {
                        x.Notified = true;
                    });
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
