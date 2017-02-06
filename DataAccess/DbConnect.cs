using DataAccess.Mappings;
using DataAccess.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace DataAccess
{
    // This class is responsible for the connection to the localDB database.
    public class DbConnect
    {
        private static ISessionFactory _sessionFactory;
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString("data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=|DataDirectory|\\InfoCom.mdf;integrated security=True"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                .BuildSessionFactory();
        }
    }

}