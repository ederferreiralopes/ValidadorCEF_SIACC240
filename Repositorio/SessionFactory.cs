using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Repositorio.Entidades;

namespace Repositorio
{
    public class SessionFactory
    {
        private static ISessionFactory session;

        public static ISessionFactory CrateSessionFactory()
        {
            if (session != null)
                return session;

            IPersistenceConfigurer configDB = MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("conexaodb"));

            var configMap = Fluently.Configure().Database(configDB)
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true)) 
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioRepositorio>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArquivoRepositorio>());

            session = configMap.BuildSessionFactory();

            return session;
        }

        public static ISession AbrirSession()
        {
            return CrateSessionFactory().OpenSession();
        }
    }
}
