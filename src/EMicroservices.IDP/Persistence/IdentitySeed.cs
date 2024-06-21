﻿using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace EMicroservices.IDP.Persistence
{
    public static class IdentitySeed
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            using var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            try
            {
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach(var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                        
                    }
                    context.SaveChanges();
                }
                if (!context.ApiScopes.Any())
                {
                    foreach (var apiscope in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(apiscope.ToEntity());

                    }
                    context.SaveChanges();
                }
                if (!context.ApiResources.Any())
                {
                    foreach (var apiResource in Config.ApiResources)
                    {
                        context.ApiResources.Add(apiResource.ToEntity());

                    }
                    context.SaveChanges();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return host;
        }
    }
}
