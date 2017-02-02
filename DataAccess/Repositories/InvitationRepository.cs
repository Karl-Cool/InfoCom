using DataAccess.Models;
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
    }
}
