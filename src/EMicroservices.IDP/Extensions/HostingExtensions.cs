using EMicroservices.IDP.Common.Domains;
using EMicroservices.IDP.Common.Domains.Repository;
using EMicroservices.IDP.Common.Repositories;
using EMicroservices.IDP.Infrastructure.Common.Repositories;
using EMicroservices.IDP.Infrastructure.Domains;
using EMicroservices.IDP.Infrastructure.Domains.Repository;
using EMicroservices.IDP.Services;
using EMicroservices.IDP.Services.EmailService;
using EMicroservices.IDP.Presentation;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EMicroservices.IDP.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();
        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddConfigurationSettings(builder.Configuration);
        builder.Services.AddScoped<IEmailSender, SmtpMailService>();
        builder.Services.ConfigureCookiePolicy();
        builder.Services.ConfigureCors();
        builder.Services.ConfigureIdentity(builder.Configuration);
        builder.Services.ConfigureIdentityServer(builder.Configuration);
        builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        builder.Services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
        builder.Services.AddControllers(config =>
        {
            config.RespectBrowserAcceptHeader = true;
            config.ReturnHttpNotAcceptable = true;
            config.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));
        }).AddApplicationPart(typeof(AssemblyReference).Assembly);
        builder.Services.ConfigureAuthentication();
        builder.Services.ConfigureAuthorization();
        builder.Services.ConfigureSwagger(builder.Configuration);
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
      
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.OAuthClientId("microservices_swagger");
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityApi");
            x.DisplayRequestDuration();
        });
        app.UseMiddleware<ErrorWrappingMiddleware>();
        app.UseRouting();
        app.UseCookiePolicy();
        app.UseIdentityServer();
        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute().RequireAuthorization("Bearer");
            endpoints.MapRazorPages().RequireAuthorization();
        });
        return app;
    }
}
