using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smoothboard_Stylers.Areas.Identity.Data;
using Smoothboard_Stylers.Data;

[assembly: HostingStartup(typeof(Smoothboard_Stylers.Areas.Identity.IdentityHostingStartup))]
namespace Smoothboard_Stylers.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Smoothboard_StylersContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Smoothboard_StylersContextConnection")));

                services.AddDefaultIdentity<Smoothboard_StylersUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<Smoothboard_StylersContext>();
            });
        }
    }
}