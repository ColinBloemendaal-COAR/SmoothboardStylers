using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smoothboard_Stylers.Areas.Identity.Data;
using Smoothboard_Stylers.Models;

namespace Smoothboard_Stylers.Data
{
    public class Smoothboard_StylersContext : IdentityDbContext<Smoothboard_StylersUser>
    {
        public Smoothboard_StylersContext(DbContextOptions<Smoothboard_StylersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            // Create Admin Role
            IdentityRole AdminRole = new IdentityRole()
            {
                Id = "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            builder.Entity<IdentityRole>().HasData(
                AdminRole
            );

            // Create Admin User
            Smoothboard_StylersUser AdminUser = new Smoothboard_StylersUser()
            {
                Id = "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                Email = "admin@smoothboardstyler.nl",
                NormalizedEmail = "ADMIN@SMOOTHBOARDSTYLER.nl",
                UserName = "admin@smoothboardstyler.nl",
                NormalizedUserName = "ADMIN@SMOOTHBOARDSTYLER.nl",
                Firstname = "Admin",
                MiddleName = "SmoothBoard",
                SurName = "Styler",
                EmailConfirmed = true,
            };
            AdminUser.PasswordHash = new PasswordHasher<Smoothboard_StylersUser>().HashPassword(AdminUser, "AdminSmoothBoardStyler2021!");


            builder.Entity<Smoothboard_StylersUser>().HasData(
                AdminUser
            );

            // Asign admin role to admin user
            builder.Entity<IdentityUserRole<string>>().HasData(
                new Microsoft.AspNetCore.Identity.IdentityUserRole<string>() { RoleId = AdminRole.Id, UserId = AdminUser.Id }
            );
        }

        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<Artikel> Artikels { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
