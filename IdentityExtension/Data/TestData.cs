using IdentityExtension.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExtension.Data
{
    public class TestData
    {
        public static async Task InitializeDB(ApplicationDbContext context, 
                                              UserManager<ApplicationUser> userManager,
                                              RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

   
            string adminRole           = "Admin";
            string adminDescription    = "Administrator Role";

            string memberRole          = "Member";
            string memberDescription   = "Member Description";

            string password             = "P@ssw0rd";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(adminRole, adminDescription, DateTime.Now));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(memberRole, memberDescription, DateTime.Now));
            }

            if (await userManager.FindByNameAsync("john@user.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "john@user.com",
                    Email = "john@user.com",
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1980, 11, 5),
                    Gender = "Male",
                    MobileContact = "+12 5555-5555",
                    HomeContact = "+23 5555-6666",
                    StreetAddress = "#08-2048, Blk 64, Yishun Avenue 6",
                    City = "Singapore",
                    PostalCode ="760064",
                    Province = "-",
                    Country = "Singapore",
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, adminRole);
                };
            }

            if (await userManager.FindByNameAsync("william@user.com") == null)
            {

                var user = new ApplicationUser
                {
                    UserName = "william@user.com",
                    Email = "william@user.com",
                    FirstName = "William",
                    LastName = "Brown",
                    DateOfBirth = new DateTime(1982, 3, 12),
                    Gender = "Male",
                    MobileContact = "+12 3333-5555",
                    HomeContact = "+23 3333-6666",
                    StreetAddress = "#02-1024, Blk 512, Woodlands Avenue 2",
                    City = "Singapore",
                    PostalCode = "700512",
                    Province = "-",
                    Country = "Singapore",
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("david@user.com") == null)
            {

                var user = new ApplicationUser
                {
                    UserName = "david@user.com",
                    Email = "david@user.com",
                    FirstName = "David",
                    LastName = "Wash",
                    DateOfBirth = new DateTime(1975, 2, 21),
                    Gender = "Male",
                    MobileContact = "+12 3333-1111",
                    HomeContact = "+23 3333-1111",
                    StreetAddress = "#04-4096, Blk 128, Bedok North Avenue 2",
                    City = "Singapore",
                    PostalCode = "700128",
                    Province = "-",
                    Country = "Singapore",
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("taylor@user.com") == null)
            {

                var user = new ApplicationUser
                {
                    UserName = "taylor@user.com",
                    Email = "taylor@user.com",
                    FirstName = "Taylor",
                    LastName = "Swift",
                    DateOfBirth = new DateTime(1985, 5, 8),
                    Gender = "Female",
                    MobileContact = "+12 2222-5555",
                    HomeContact = "+23 2222-6666",
                    StreetAddress = "#02-128, Blk 256, Angmokio Avenue 2",
                    City = "Singapore",
                    PostalCode = "700512",
                    Province = "-",
                    Country = "Singapore",
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

            if (await userManager.FindByNameAsync("brain@user.com") == null)
            {

                var user = new ApplicationUser
                {
                    UserName = "brain@user.com",
                    Email = "brain@user.com",
                    FirstName = "Brain",
                    LastName = "Greene",
                    DateOfBirth = new DateTime(1982, 3, 12),
                    Gender = "Male",
                    MobileContact = "+12 3333-8888",
                    HomeContact = "+23 3333-8888",
                    StreetAddress = "#02-1024, Blk 125, Bedok South Avenue 6",
                    City = "Singapore",
                    PostalCode = "700125",
                    Province = "-",
                    Country = "Singapore",
                };

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }

        }
    }
}
