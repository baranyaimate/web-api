using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using web_api.Models;

namespace web_api;

public static class FluentNHibernateHelper
{

    public static NHibernate.ISession OpenSession()
    {
        const string connectionString = "Server=localhost;Database=web_api;Trusted_Connection=True;MultipleActiveResultSets=True";

        ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(connectionString).ShowSql()
                )
                .Mappings(m => m.FluentMappings
                        .AddFromAssemblyOf<Address>()
                        .AddFromAssemblyOf<Order>()
                        .AddFromAssemblyOf<Product>()
                        .AddFromAssemblyOf<User>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                    .Execute(false, true))
                .BuildSessionFactory();

        return sessionFactory.OpenSession();
    }

}