using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using payCoreHW3.Context;

namespace payCoreHW3.StartUpExtensions;

public static class NHibernateExtension
{
    public static IServiceCollection AddHibernatePostgreSql(this IServiceCollection services, string connectionString)
    {
        var mapper = new ModelMapper();
        mapper.AddMappings(typeof(NHibernateExtension).Assembly.ExportedTypes);
        HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        var configuration = new Configuration();
        configuration.DataBaseIntegration(c =>
        {
            c.Dialect<PostgreSQLDialect>();
            c.ConnectionString = connectionString;
            c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            c.SchemaAction = SchemaAutoAction.Update; //i use Update instead of Validate
            c.LogFormattedSql = true;
            c.LogSqlInConsole = true;
        });
        configuration.AddMapping(domainMapping);

        var sessionFactory = configuration.BuildSessionFactory();
        services.AddSingleton(sessionFactory);
        services.AddScoped(factory => sessionFactory.OpenSession());

        services.AddScoped<IMapperSession, MapperSession>();

        return services;
    }
}