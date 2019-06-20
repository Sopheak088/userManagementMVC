using iThinking.UserCenter.Common;
using iThinking.UserCenter.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace iThinking.UserCenter.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UserCenterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserCenterDbContext context)
        {
            AddProject(context);
            AddUser(context);
        }

        #region AddProject

        private void AddProject(UserCenterDbContext context)
        {
            if (context.AspNetProjects.Where(m => m.Id == "UserApp").Count() == 0)
            {
                List<ApplicationProject> listProject = new List<ApplicationProject>()
                {
                    new ApplicationProject() { Id = "UserApp", Name="User Management", Description="Quản lý người dùng", CreatedBy="Admin", CreatedDate=DateTime.Now, UpdatedBy=null, UpdatedDate=null }
                };
                foreach (var item in listProject)
                {
                    context.AspNetProjects.Add(item);
                }
                context.SaveChanges();
            }
        }

        #endregion AddProject

        #region AddUser

        private void AddUser(UserCenterDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var manager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new UserCenterDbContext()));

                var roleManager = new RoleManager<ApplicationRole, string>(new RoleStore<ApplicationRole, string, ApplicationUserRole>(new UserCenterDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "huynhquy9x@gmail.com",
                    EmailConfirmed = true,
                    Birthday = DateTime.ParseExact("28/04/1992", "dd/MM/yyyy", null),
                    FirstName = "Quy",
                    LastName = "Huynh Van",
                    PhoneNumber = "0976656454",
                    AvatarPath = null,
                    Gender = Gender.Male,
                    UploadFolder = "huynhquy9x_gmail.com",
                    About = "Software developer",
                    PhoneNumberConfirmed = true,
                    Address = "Ho Chi Minh City",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Application"
                };

                manager.Create(user, "123@Abcc");

                if (!roleManager.Roles.Where(m => m.Name == "Admin").Any())
                {
                    roleManager.CreateAsync(new ApplicationRole("Admin", "Admin", "Allow Full Access", "Admin", null));
                }

                var adminUser = manager.FindByEmail("huynhquy9x@gmail.com");

                manager.AddToRoles(adminUser.Id, new string[] { "Admin" });
            }
        }

        #endregion AddUser
    }
}