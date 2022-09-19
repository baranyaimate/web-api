using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using web_api.Models;

namespace web_api;

public class FluentNHibernateHelper
{
    public static NHibernate.ISession OpenSession()
    {
        string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;MultipleActiveResultSets=True";

        ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionString).ShowSql()
            )
            .Mappings(m =>
                m.FluentMappings
                    .AddFromAssemblyOf<Product>())
            .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(false, false))
            .BuildSessionFactory();

        return sessionFactory.OpenSession();

    }
}