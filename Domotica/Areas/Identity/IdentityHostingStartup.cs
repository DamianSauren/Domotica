using System;
using Domotica.Areas.Identity.Data;
using Domotica.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Domotica.Areas.Identity.IdentityHostingStartup))]
namespace Domotica.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DomoticaContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DomoticaContextConnection")));

                services.AddDefaultIdentity<DomoticaUser>(options => options.SignIn.RequireConfirmedAccount = false) //Set  to true for email confirmation
                    .AddEntityFrameworkStores<DomoticaContext>();
            });
        }
    }
}