using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Controllers
{
    public static class InitializeBaseData
    {
        public static async Task RoleInitialize(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            string[] roleNames = { Roles.ADMIN.ToString(), Roles.MANAGER.ToString(), Roles.ADVISOR.ToString(), Roles.USER.ToString() };
            foreach (string roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    Role identityRole = new Role { Name = roleName };
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }
        public static async Task UserInitialize(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var admin = new User
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != admin.Id))
            {
                var adminUser = await userManager.FindByEmailAsync(admin.Email);
                if (adminUser == null)
                {
                    await userManager.CreateAsync(admin, "admin@123");
                    await userManager.AddToRoleAsync(admin, Roles.ADMIN.ToString());
                    await userManager.AddToRoleAsync(admin, Roles.MANAGER.ToString());
                    await userManager.AddToRoleAsync(admin, Roles.USER.ToString());
                }
            }
        }
        public static async Task InsuranceTypeInitialize(ApplicationDbContext context)
        {
            InsuranceType[] insuranceTypes = {
                new InsuranceType { Name = "Life Insurance",
                IconUrl = "icon-10-light.png",
                    ImageUrl = "lifeinsurance.jpg",
                    Description = "Protect your loved ones and secure their financial future in case the unexpected happens. ",
                    CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now},
                new InsuranceType { Name = "Medical Insurance",
                IconUrl = "icon-01-light.png",
                    ImageUrl = "medicalinsurance.jpg",
                    Description="Medical insurance provides financial protection and coverage for medical expenses, including doctor visits, hospital stays, and prescription medications.",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now},
                new InsuranceType { Name = "Vehicle Insurance",
                IconUrl = "icon-08-light.png",
                    ImageUrl = "motorinsurance.jpg",
                    Description = "Vehicle insurance provides coverage for your car or other vehicles against accidents, theft, and damage.",
                    CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now},
                new InsuranceType { Name = "Home Insurance",
                IconUrl = "icon-05-light.png",
                    ImageUrl = "homeinsurance.jpg",
                    Description = "Home insurance offers protection for your property and belongings against risks such as fire, theft, and natural disasters. ",
                    CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now},
            };
            foreach (InsuranceType insuranceType in insuranceTypes)
            {
                var insuranceTypeTemp = await context.InsuranceTypes.FirstOrDefaultAsync(i => i.Name == insuranceType.Name);
                if (insuranceTypeTemp == null)
                {
                    await context.InsuranceTypes.AddAsync(insuranceType);
                    await context.SaveChangesAsync();
                }
            }
        }
        public static async Task InsurancePlanInitialize(ApplicationDbContext context)
        {
            InsurancePlan[] insurancePlans =
            {
                new InsurancePlan{
                        Name = "Life Plan Monthly",
                        Description = "This is Life Plan Monthly",
                        Premium = 100.00m,
                        TermType = TermType.Monthly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 1 // Life
                    },
                new InsurancePlan{
                        Name = "Life Plan Quarterly",
                        Description = "This is Life Plan Quarterly",
                        Premium = 100.00m,
                        TermType = TermType.Quarterly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 1 // Life
                    },
                new InsurancePlan{
                        Name = "Life Plan HalfYearly",
                        Description = "This is Life Plan HalfYearly",
                        Premium = 100.00m,
                        TermType = TermType.HalfYearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 1 // Life
                    },
                new InsurancePlan{
                        Name = "Life Plan Yearly",
                        Description = "This is Life Plan Yearly",
                        Premium = 100.00m,
                        TermType = TermType.Yearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 1 // Life
                    },


                new InsurancePlan{
                        Name = "Medical Plan Monthly",
                        Description = "This is Medical Plan Monthly",
                        Premium = 100.00m,
                        TermType = TermType.Monthly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 2 
                    },
                new InsurancePlan{
                        Name = "Medical Plan Quarterly",
                        Description = "This is Medical Plan Quarterly",
                        Premium = 100.00m,
                        TermType = TermType.Quarterly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 2 
                    },
                new InsurancePlan{
                        Name = "Medical Plan HalfYearly",
                        Description = "This is Medical Plan HalfYearly",
                        Premium = 100.00m,
                        TermType = TermType.HalfYearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 2
                    },
                new InsurancePlan{
                        Name = "Medical Plan Yearly",
                        Description = "This is Medical Plan Yearly",
                        Premium = 100.00m,
                        TermType = TermType.Yearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 2 
                    },


                new InsurancePlan{
                        Name = "Vehicle Plan Monthly",
                        Description = "This is Vehicle Plan Monthly",
                        Premium = 100.00m,
                        TermType = TermType.Monthly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 3 // Vehicle
                    },
                new InsurancePlan{
                        Name = "Vehicle Plan Quarterly",
                        Description = "This is Vehicle Plan Quarterly",
                        Premium = 100.00m,
                        TermType = TermType.Quarterly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 3 // Vehicle
                    }, new InsurancePlan{
                        Name = "Vehicle Plan HalfYearly",
                        Description = "This is Vehicle Plan HalfYearly",
                        Premium = 100.00m,
                        TermType = TermType.HalfYearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 3 // Vehicle
                    },
                new InsurancePlan{
                        Name = "Vehicle Plan Yearly",
                        Description = "This is Vehicle Plan Yearly",
                        Premium = 100.00m,
                        TermType = TermType.Yearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 3 // Vehicle
                    },


                new InsurancePlan{
                        Name = "Home Plan Monthly",
                        Description = "This is Home Plan Monthly",
                        Premium = 100.00m,
                        TermType = TermType.Monthly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 4 // Home
                    },
                new InsurancePlan{
                        Name = "Home Plan Quarterly",
                        Description = "This is Home Plan Quarterly",
                        Premium = 100.00m,
                        TermType = TermType.Quarterly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 4 // Home
                    },
                new InsurancePlan{
                        Name = "Home Plan HalfYearly",
                        Description = "This is Home Plan HalfYearly",
                        Premium = 100.00m,
                        TermType = TermType.HalfYearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 4 // Home
                    },
                new InsurancePlan{
                        Name = "Home Plan Yearly",
                        Description = "This is Home Plan Yearly",
                        Premium = 100.00m,
                        TermType = TermType.Yearly,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        InsuranceTypeId = 4 // Home
                    }
            };
            foreach (var insurancePlan in insurancePlans)
            {
                var insurancePlanTemp = await context.InsurancePlans.FirstOrDefaultAsync(i => i.Name == insurancePlan.Name);
                if (insurancePlanTemp == null)
                {
                    await context.InsurancePlans.AddAsync(insurancePlan);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
