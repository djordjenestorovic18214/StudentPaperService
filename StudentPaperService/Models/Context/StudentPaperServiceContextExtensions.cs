using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace StudentPaperService.Models.Context
{
    public static class StudentPaperServiceContextExtensions
    {
        public static bool AllMigrationsApplied(this StudentPaperServiceContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static StudentPaperServiceContext EnsureDatabaseSeeded(this StudentPaperServiceContext context)
        {
            if (!context.Roles.Any())
            {
                seedRolesTable(context);
            }

            if (!context.Users.Any())
            {
                seedUsersTable(context);
            }

            if (!context.UserRoles.Any())
            {
                seedUserRolesTable(context);
            }

            return context;
        }

        private static void seedUserRolesTable(StudentPaperServiceContext context)
        {
            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                UserId = "1",
                RoleId = "1"
            });

          
            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                UserId = "2",
                RoleId = "3"
            });

            context.SaveChanges();
        }

        private static void seedRolesTable(StudentPaperServiceContext context)
        {
            context.Roles.Add(new IdentityRole()
            {
                Id = "1",
                Name = "admin"
            });

            context.Roles.Add(new IdentityRole()
            {
                Id = "2",
                Name = "profesor"
            });

            context.Roles.Add(new IdentityRole()
            {
                Id = "3",
                Name = "student"
            });
            context.SaveChanges();
        }


        private static void seedUsersTable(StudentPaperServiceContext context)
        {
            context.Users.Add(
                new Admin() { Id = "1", AccessFailedCount = 0, ConcurrencyStamp = "effe676e-4bea-460a-8e48-53a7a535328c", Email = null, EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "admin", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "admin" });
            //context.Users.Add(
            //    new Admin() { Id = "3", AccessFailedCount = 0, ConcurrencyStamp = "effe676e-4bea-460a-8e48-53a7a535328c", Email = null, EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "admin2", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "admin2" });

            context.Users.Add(
               new Student() { Id = "2", AccessFailedCount = 0, ConcurrencyStamp = "aa052285-b8c9-46a9-92b9-f08e81fe0258", Email = null, EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student", FirstName = "Djole", LastName = "Nestorovic", IndexNumber = "182/14"});            

            context.SaveChanges();
        }
    }
}
