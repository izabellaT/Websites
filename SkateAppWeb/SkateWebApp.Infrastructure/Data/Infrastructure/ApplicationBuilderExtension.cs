using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SkateAppWeb.Infrastructure.Data;
using SkateWebApp.Infrastructure.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateWebApp.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrand(dataBrand);

            return app;
        }

        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category {CategoryName="Complete Skateboard"},
                new Category {CategoryName="Wheel"},
                new Category {CategoryName="Accessory"},
                new Category {CategoryName="Truck"},
                new Category {CategoryName="Deck"},
                new Category {CategoryName="Griptape"},
                new Category {CategoryName="Tool"},
                new Category {CategoryName="Bearing"},
                new Category {CategoryName="Skateboard"}
            });
            dataCategory.SaveChanges();
        }

        private static void SeedBrand(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }
            dataBrand.Brands.AddRange(new[]
            {
                new Brand {BrandName="Brandless"},
                new Brand {BrandName="CCS"},
                new Brand {BrandName="Santa Cruz"},
                new Brand {BrandName="Girl"},
                new Brand {BrandName="Zero"},
                new Brand {BrandName="Creature"},
                 new Brand {BrandName="Plan B"},
                new Brand {BrandName="Almost"},
                new Brand {BrandName="Baker"},
                new Brand {BrandName="Alien Workshop"}
                 
            });
            dataBrand.SaveChanges();
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "fast";
                user.LastName = "wheels";
                user.PhoneNumber = "0888888888";
                user.Address = "Pernik";
                user.UserName = "fastWheels";
                user.Email = "fast@wheels.com";

                var result = await userManager.CreateAsync(user, "FastWheels");
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
