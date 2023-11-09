using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Infrastructure.SeederData;
using SystemTheLastBugSpa.Data;

namespace System.WebApi.Extensions
{
    public static class HostDatabaseExtension
    {
        public static IHost InitDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AplicationDataContext>();

            // now we have the DbContext. Run migrations
            Log.Warning("Applying migrations");
            context.Database.Migrate();
            Log.Warning("Applying migrations done");

            // -------------
            //  Seeder here
            // -------------
            Log.Warning("Applying Seeads");

           
            Log.Warning("Applying Seeads done");
            return host;
        }
    }
}
