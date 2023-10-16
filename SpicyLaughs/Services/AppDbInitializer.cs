using Microsoft.AspNetCore.Identity;
using SpiceyLaughs.Model;
using SpiceyLaughs.Static;

namespace SpiceyLaughs.Services
{
    public static class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var ServiceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = ServiceScope.ServiceProvider.GetService<AppDbContext>();

            context.Database.EnsureCreated();

            
            if(!context.Categories.Any())
            {
                context.Categories.AddRange(new List<Category>()
                {
                    new Category()
                    {
                        Title = "North Indian",
                        Description = "North Indian cuisine is renowned for its rich and diverse flavors. It includes a wide array of dishes, often characterized by the use of aromatic spices, dairy products, and bread such as naan and paratha. Popular dishes from this region include biryani, butter chicken, kebabs, and various lentil-based preparations.",
                        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/eb/North_India.svg/1280px-North_India.svg.png"
                    },
                    new Category()
                    {
                        Title = "South Indian",
                        Description = "South Indian cuisine is known for its use of rice as a staple and a wide variety of fermented rice and lentil dishes such as dosa and idli. Coconut, tamarind, and an array of spices are prominent in South Indian cooking.",
                        ImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/South_India.svg/1280px-South_India.svg.png"
                    },
                    new Category()
                    {
                        Title = "Maharashtrian",
                        Description = "Maharashtrian cuisine is a diverse and flavorful culinary tradition from the Indian state of Maharashtra. It features a mix of spicy, sweet, and tangy flavors, often using ingredients like coconut, jaggery, and tamarind.",
                        ImageURL = "https://as2.ftcdn.net/v2/jpg/03/16/76/07/1000_F_316760742_IO5R2QPhdiKkgdtzC12RWJuo2wgjNr2V.jpg"
                    }

                }) ;
                context.SaveChanges();
            }
            if (!context.Dishes.Any())
            {
                context.Dishes.AddRange(new List<Dish>()
                {
                    new Dish()
                    {
                        Title = "Pav Bhaji",
                        Description="Pav Bhaji is a popular Indian street food dish consisting of a spicy and flavorful mixed vegetable curry (bhaji) served with soft buttered rolls (pav), typically garnished with chopped onions and lemon juice.",
                        Price = 500,
                        ImageURL = "https://www.cubesnjuliennes.com/wp-content/uploads/2020/07/Instant-Pot-Mumbai-Pav-Bhaji.jpg",
                        DietaryPrefernce = DietaryPrefernce.Veg,
                        Available = true,
                        CategoryId = 3,
                        Calories = 370,
                        SpiceLevel = SpiceLevel.Medium,
                        Time = 30
                    },
                    new Dish()
                    {
                        Title = "Masala Dosa",
                        Description="Masala Dosa is a South Indian delicacy, featuring a thin, crispy rice crepe filled with a flavorful spiced potato mixture. Served with chutneys and sambar.",
                        Price = 100,
                        ImageURL = "https://t3.ftcdn.net/jpg/03/97/46/66/240_F_397466683_4U8hMaUgWdFPNc8KMKUQ4aH2qR1yG0sA.jpg",
                        DietaryPrefernce = DietaryPrefernce.Veg,
                        Available = true,
                        CategoryId = 2,
                        SpiceLevel= SpiceLevel.Mild,
                        Calories = 150,
                        Time = 15

                    },
                     new Dish()
                    {
                        Title = "Chole Bhature",
                        Description="Chole Bhature is a North Indian dish that pairs spicy chickpea curry (chole) with deep-fried bread (bhature). It's a hearty and flavorful combination often served for breakfast or as a satisfying meal.",
                        Price = 200,
                        ImageURL = "https://as2.ftcdn.net/v2/jpg/03/50/25/05/1000_F_350250570_EqclcN4CpkeiD7BvflaSxaMwpJ7b7PNC.jpg",
                        DietaryPrefernce = DietaryPrefernce.Veg,
                        Available = true,
                        CategoryId = 1,
                        SpiceLevel = SpiceLevel.Medium,
                        Calories = 600,
                        Time = 25
                    }

                }) ;
                context.SaveChanges();
            }
        }               

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Role = UserRoles.Admin,
                    };
                    await userManager.CreateAsync(newAdminUser, "Qqwer1234@");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser
                    {
                        FullName = "App User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Role = UserRoles.User
                    };
                    await userManager.CreateAsync(newAppUser, "Qqwer1234@");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
