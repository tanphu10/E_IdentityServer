using EMicroservice.IDP.Common;
using EMicroservice.IDP.Entities;
using EMicroservice.IDP.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace EMicroservice.IDP.Extensions
{
    public static class ServiceExtensions
    {

        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection(nameof(SMTPEmailSetting))
              .Get<SMTPEmailSetting>();
            services.AddSingleton(emailSettings);

            return services;
        }
        public static void AddAppConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });

        }
        public static void ConfigureSerilog(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, configuration) =>
            {
                var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
                var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";
                var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
                var username = context.Configuration.GetValue<string>("ElasticConfiguration:Username");
                var password = context.Configuration.GetValue<string>("ElasticConfiguration:Password");
                configuration
                    .WriteTo.Debug()
                    .WriteTo.Console(outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                    {
                        IndexFormat = $"tanphulogs-{applicationName}-{environmentName}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfReplicas = 1,
                        NumberOfShards = 2,
                        ModifyConnectionSettings = x => x.BasicAuthentication(username, password)
                    })
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Environment", environmentName)
                    .Enrich.WithProperty("Application", applicationName)
                    .ReadFrom.Configuration(context.Configuration);
            }
            );

        }

        public static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetConnectionString("IdentitySqlConnection");
            //if (connectionStrings == null) throw Exception(nameof(connectionStrings));
            services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            }).AddDeveloperSigningCredential()
          //.AddInMemoryIdentityResources(Config.IdentityResources)
          //.AddInMemoryApiScopes(Config.ApiScopes)
          //.AddInMemoryClients(Config.Clients)
          //.AddInMemoryApiResources(Config.ApiResources)
          //.AddTestUsers(TestUsers.Users)
          .AddConfigurationStore(opt =>
          {
              opt.ConfigureDbContext = c => c.UseSqlServer(connectionStrings, builder => builder.MigrationsAssembly("EMicroservice.IDP"));
          })
          .AddOperationalStore(opt =>
          {
              opt.ConfigureDbContext = c => c.UseSqlServer(connectionStrings, builder => builder.MigrationsAssembly("EMicroservice.IDP"));
          })
          .AddAspNetIdentity<User>().AddProfileService<IdentityProfileService>();
          ;
        }
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("IdentitySqlConnection");
            services
                .AddDbContext<IdentityContext>(options => options
                    .UseSqlServer(connectionString))
                .AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.User.RequireUniqueEmail = true;
                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    opt.Lockout.MaxFailedAccessAttempts = 3;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader());
                });
        }
    }
}
