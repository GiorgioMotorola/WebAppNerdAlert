using Microsoft.AspNetCore.Identity;
using WebAppNerdAlert.Data.Enum;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Hobbies.Any())
                {
                    context.Hobbies.AddRange(new List<Hobby>()
                    {
                        new Hobby()
                        {
                            Title = "TableTop Trio",
                            Image = "https://cdn.thisiswhyimbroke.com/thumb/dichroic-glass-d20-dice_400x333.jpg",
                            Description = "Boys rollin Dice.",
                            HobbyCategory = HobbyCategory.TTRPG,
                            Address = new Address()
                            {
                                Street = "123 Comic Book Lane",
                                City = "Erlanger",
                                State = "KY"
                            }
                         },
                        new Hobby()
                        {
                            Title = "Book Worms",
                            Image = "https://c.files.bbci.co.uk/C992/production/_102720615_gettyimages-149565765.jpg",
                            Description = "We don't actually eat the books. We read them",
                            HobbyCategory = HobbyCategory.Media,
                            Address = new Address()
                            {
                                Street = "123 Barnes and Noble",
                                City = "Bookton",
                                State = "KY"
                            }
                        },
                        new Hobby()
                        {
                            Title = "Computer Builders Society",
                            Image = "https://www.digitaltrends.com/wp-content/uploads/2021/11/pcbuiltkid01.jpg?fit=720%2C720&p=1",
                            Description = "Processing...",
                            HobbyCategory = HobbyCategory.Technology,
                            Address = new Address()
                            {
                                Street = "123 RAM Blvd",
                                City = "Tech",
                                State = "CA"
                            }
                        },
                        new Hobby()
                        {
                            Title = "Hearthstoners",
                            Image = "https://images.blz-contentstack.com/v3/assets/bltc965041283bac56c/bltac2ba496972db9d1/604a8ad26968b53d529ee7fe/play-hs_3_play-cards.jpg",
                            Description = "We like Hearthstone and other stuff",
                            HobbyCategory = HobbyCategory.VideoGames,
                            Address = new Address()
                            {
                                Street = "Online",
                                City = "Discord",
                                State = "DC"
                            }
                        },
                        new Hobby()
                        {
                            Title = "Stamp Collector Nation",
                            Image = "https://www.westcoaststamps.com/wp-content/uploads/2019/10/stamp-collecting-guide.jpg",
                            Description = "Gotta Catch Em All!",
                            HobbyCategory = HobbyCategory.Misc,
                            Address = new Address()
                            {
                                Street = "123 Poke Mon",
                                City = "Woodtown",
                                State = "OK"
                            }
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Events.Any())
                {
                    context.Events.AddRange(new List<Event>()
                    {
                        new Event()
                        {
                            Title = "Curse of Strahd Campaign",
                            Image = "https://images.ctfassets.net/swt2dsco9mfe/1VfE1egQ1KTm17xXK6FHa9/a8b66026ed61e41a9eb9906853980d52/COSR-product-background.jpg?q=70&w=540&h=350",
                            Description = "He is a mean guy. Lets kill him!",
                            EventCategory = EventCategory.InPerson,
                            Address = new Address()
                            {
                                Street = "123 Baxters Place",
                                City = "Barovia",
                                State = "ShadowFell"
                            }
                         },
                        new Event()
                        {
                            Title = "Girls Night In: Vinyl Listening Party",
                            Image = "https://www.electronicbeats.net/app/uploads/2016/11/beer-vinyl.jpg",
                            Description = "Just a couple of girls havin' a nice drink or two",
                            EventCategory = EventCategory.Hybrid,
                            Address = new Address()
                            {
                                Street = "123 Bills Place",
                                City = "Lmao",
                                State = "KY"
                            }
                        },
                        new Event()
                        {
                            Title = "The ShareBros Share Code",
                            Image = "https://global-uploads.webflow.com/62b397ed0ff18cefd722ad0c/62e81ca0304e5174b6fa0d78_coding_for_business.jpg",
                            Description = "Working on a Goat Screaming App. Come help debug and maybe Duncan will hang out, too",
                            EventCategory = EventCategory.Corporate,
                            Address = new Address()
                            {
                                Street = "123 Keiths Place",
                                City = "Lol",
                                State = "KY"
                            }
                        },
                        new Event()
                        {
                            Title = "Stardew Valley: Race to see who Abigail marries first",
                            Image = "https://www.pockettactics.com/wp-content/uploads/2021/07/stardew-valley-abigail-2.jpg",
                            Description = "She likes to eat rocks. Why not instead give her a big rock on a ring?",
                            EventCategory = EventCategory.Virtual,
                            Address = new Address()
                            {
                                Street = "Online",
                                City = "Discord",
                                State = "DC"
                            }
                        },
                        new Event()
                        {
                            Title = "Bird Watching for Old Guys but love charity",
                            Image = "https://www.allaboutbirds.org/news/wp-content/uploads/2020/07/STanager-Shapiro-ML.jpg",
                            Description = "Birds",
                            EventCategory = EventCategory.Fundraising,
                            Address = new Address()
                            {
                                Street = "123 The Woods",
                                City = "Woodtown",
                                State = "OK"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
       }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "mikesweatherford@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "mweatherford",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@user.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

    }
}
