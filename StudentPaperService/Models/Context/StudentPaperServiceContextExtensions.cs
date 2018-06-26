using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
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
            context.UserRoles.AddRange(
                new IdentityUserRole<string>(){ UserId = "11", RoleId = "1" },
                new IdentityUserRole<string>() { UserId = "21", RoleId = "2" },
                new IdentityUserRole<string>() { UserId = "22", RoleId = "2" },
                new IdentityUserRole<string>() { UserId = "23", RoleId = "2" },
                new IdentityUserRole<string>() { UserId = "24", RoleId = "2" },
                new IdentityUserRole<string>() { UserId = "25", RoleId = "2" },
                new IdentityUserRole<string>() { UserId = "31", RoleId = "3" },
                new IdentityUserRole<string>() { UserId = "32", RoleId = "3" },
                new IdentityUserRole<string>() { UserId = "33", RoleId = "3" },
                new IdentityUserRole<string>() { UserId = "34", RoleId = "3" },
                new IdentityUserRole<string>() { UserId = "35", RoleId = "3" },
                new IdentityUserRole<string>() { UserId = "36", RoleId = "3" }
            );

            context.SaveChanges();
        }

        private static void seedRolesTable(StudentPaperServiceContext context)
        {
            context.Roles.AddRange(
                new IdentityRole(){ Id = "1", Name = "admin" }, 
                new IdentityRole() { Id = "2", Name = "profesor" }, 
                new IdentityRole() { Id = "3", Name = "student" }
                );

            context.SaveChanges();
        }

        private static void seedUsersTable(StudentPaperServiceContext context)
        {
            List<Professor> professors = new List<Professor>();
            List<Subject> subjects = new List<Subject>();
            List<ProfessorSubject> professorSubjects = new List<ProfessorSubject>();
            List<Student> students = new List<Student>();
            List<SeminarPaper> seminarPapers = new List<SeminarPaper>();

            seedData(out professors, out subjects, out professorSubjects, out students, out seminarPapers);

            Admin admin = new Admin() { Id = "11", AccessFailedCount = 0, ConcurrencyStamp = "b58d0834-4428-4b8b-8585-54f91a26ba71", Email = null, EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "admin", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "admin" };
                
            context.Users.Add(admin);
            context.SaveChanges();

            context.Users.AddRange(professors);
            context.SaveChanges();

            context.Users.AddRange(students);
            context.SaveChanges();

            context.Subjects.AddRange(subjects);
            context.SaveChanges();

            context.ProfessorSubjects.AddRange(professorSubjects);
            context.SaveChanges();

            context.SeminarPapers.AddRange(seminarPapers);
            context.SaveChanges();            
        }

        private static void seedData(out List<Professor> professors, out List<Subject> subjects, out List<ProfessorSubject> professorSubjects, out List<Student> students, out List<SeminarPaper> seminarPapers)
        {
            professors = new List<Professor>();
            subjects = new List<Subject>();
            professorSubjects = new List<ProfessorSubject>();
            students = new List<Student>();
            seminarPapers = new List<SeminarPaper>();

            //sifra za profesora Admin.123.
            Professor p1 = new Professor() { FirstName = "Душан", LastName = "Бараћ", Email = "dusan.barac@fon.bg.ac.rs", Id = "21", AccessFailedCount = 0, ConcurrencyStamp = "976d6c23-12e5-4acb-8130-601e73a0f77b", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "profesor1", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "profesor1" };
            Professor p2 = new Professor() { FirstName = "Марко", LastName = "Петровић", Email = "marko.petrovic@fon.bg.ac.rs", Id = "22", AccessFailedCount = 0, ConcurrencyStamp = "976d6c23-12e5-4acb-8130-601e73a0f77b", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "profesor2", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "profesor2" };
            Professor p3 = new Professor() { FirstName = "Оливера", LastName = "Михић", Email = "olivera.mihic@fon.bg.ac.rs", Id = "23", AccessFailedCount = 0, ConcurrencyStamp = "976d6c23-12e5-4acb-8130-601e73a0f77b", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "profesor3", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "profesor3" };
            Professor p4 = new Professor() { FirstName = "Владан", LastName = "Девеџић", Email = "vladan.devedzic@fon.bg.ac.rs", Id = "24", AccessFailedCount = 0, ConcurrencyStamp = "976d6c23-12e5-4acb-8130-601e73a0f77b", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "profesor4", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "profesor4" };
            Professor p5 = new Professor() { FirstName = "Зоран", LastName = "Радојичић", Email = "zoran.radojicic@fon.bg.ac.rs", Id = "25", AccessFailedCount = 0, ConcurrencyStamp = "976d6c23-12e5-4acb-8130-601e73a0f77b", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "profesor5", PasswordHash = "AQAAAAEAACcQAAAAEHBL8bFd9GJAYlw8zQiyUsq4Qg+7/1XOo3XBQwUwx50VIlJcoqi8GgrMwGF5DzHFiw==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "ac1d41a1-6a1b-43d5-83d0-1eb64223e354", TwoFactorEnabled = false, UserName = "profesor5" };

            professors.Add(p1);
            professors.Add(p2);
            professors.Add(p3);
            professors.Add(p4);
            professors.Add(p5);

            Subject s1 = new Subject() { Name = "Интернет технологије" };
            Subject s2 = new Subject() { Name = "Електронско пословање" };
            Subject s3 = new Subject() { Name = "Програмски језици" };
            Subject s4 = new Subject() { Name = "Математика 1" };
            Subject s5 = new Subject() { Name = "Математика 2" };
            Subject s6 = new Subject() { Name = "Програмирање 2" };
            Subject s7 = new Subject() { Name = "Интелигентни системи" };
            Subject s8 = new Subject() { Name = "Статистика" };

            subjects.Add(s1);
            subjects.Add(s2);
            subjects.Add(s3);
            subjects.Add(s4);
            subjects.Add(s5);
            subjects.Add(s6);
            subjects.Add(s7);
            subjects.Add(s8);

            ProfessorSubject ps1 = new ProfessorSubject() { Professor = p1, Subject = s1 };
            ProfessorSubject ps2 = new ProfessorSubject() { Professor = p1, Subject = s2 };
            ProfessorSubject ps3 = new ProfessorSubject() { Professor = p2, Subject = s3 };
            ProfessorSubject ps4 = new ProfessorSubject() { Professor = p3, Subject = s4 };
            ProfessorSubject ps5 = new ProfessorSubject() { Professor = p3, Subject = s5 };
            ProfessorSubject ps6 = new ProfessorSubject() { Professor = p4, Subject = s6 };
            ProfessorSubject ps7 = new ProfessorSubject() { Professor = p4, Subject = s7 };
            ProfessorSubject ps8 = new ProfessorSubject() { Professor = p5, Subject = s8 };

            professorSubjects.Add(ps1);
            professorSubjects.Add(ps2);
            professorSubjects.Add(ps3);
            professorSubjects.Add(ps4);
            professorSubjects.Add(ps5);
            professorSubjects.Add(ps6);
            professorSubjects.Add(ps7);
            professorSubjects.Add(ps8);

            //sifra za studenta Korisnik.123.
            Student st1 = new Student() { FirstName = "Валентина", LastName = "Љубисављевић", IndexNumber = "0095/2014", Email = "valentina.ljubisavljevic@gmail.com", Id = "31", AccessFailedCount = 0, ConcurrencyStamp = "b2c6c314-d80d-43ff-9b45-51124a38ba49", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student1", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student1"};
            Student st2 = new Student() { FirstName = "Ђорђе", LastName = "Несторовић", IndexNumber = "0182/2014", Email = "djordje.nestorovic@gmail.com", Id = "32", AccessFailedCount = 0, ConcurrencyStamp = "b2c6c314-d80d-43ff-9b45-51124a38ba49", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student2", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student2" };
            Student st3 = new Student() { FirstName = "Филип", LastName = "Фуртула", IndexNumber = "0155/2014", Email = "filip.furtula@gmail.com", Id = "33", AccessFailedCount = 0, ConcurrencyStamp = "b2c6c314-d80d-43ff-9b45-51124a38ba49", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student3", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student3" };
            Student st4 = new Student() { FirstName = "Филип", LastName = "Филиповић", IndexNumber = "094/2014", Email = "filip.filipovic@gmail.com", Id = "34", AccessFailedCount = 0, ConcurrencyStamp = "b2c6c314-d80d-43ff-9b45-51124a38ba49", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student4", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student4" };
            Student st5 = new Student() { FirstName = "Марко", LastName = "Стевић", IndexNumber = "0024/2014", Email = "marko.stevic@gmail.com", Id = "35", AccessFailedCount = 0, ConcurrencyStamp = "b2c6c314-d80d-43ff-9b45-51124a38ba49", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student5", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student5" };
            Student st6 = new Student() { FirstName = "Јелена", LastName = "Марјановић", IndexNumber = "0092/2014", Email = "jelena.marjanovic@gmail.com", Id = "36", AccessFailedCount = 0, ConcurrencyStamp = "b2c6c314-d80d-43ff-9b45-51124a38ba49", EmailConfirmed = false, LockoutEnabled = true, LockoutEnd = null, NormalizedEmail = null, NormalizedUserName = "student6", PasswordHash = "AQAAAAEAACcQAAAAEOkysDm1lWmQrKSFzGPFZvK60m9dfY3isNrvazCEw9+cPg3ynBUK8tPH9hnK4hAYjg==", PhoneNumber = null, PhoneNumberConfirmed = false, SecurityStamp = "95f456ec-7b11-4549-a193-9cca49ef4a38", TwoFactorEnabled = false, UserName = "student6" };

            students.Add(st1);
            students.Add(st2);
            students.Add(st3);
            students.Add(st4);
            students.Add(st5);
            students.Add(st6);

            SeminarPaper sp1 = new SeminarPaper() { Name = "Семинарски 1 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps1, Student = st1, PaperFile = new byte[20] };
            SeminarPaper sp2 = new SeminarPaper() { Name = "Семинарски 2 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps1, Student = st2, PaperFile = new byte[20] };
            SeminarPaper sp3 = new SeminarPaper() { Name = "Семинарски 3 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps2, Student = st2, PaperFile = new byte[20] };
            SeminarPaper sp4 = new SeminarPaper() { Name = "Семинарски 4 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps4, Student = st2, PaperFile = new byte[20] };
            SeminarPaper sp5 = new SeminarPaper() { Name = "Семинарски 5 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps3, Student = st3, PaperFile = new byte[20] };
            SeminarPaper sp6 = new SeminarPaper() { Name = "Семинарски 6 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps4, Student = st4, PaperFile = new byte[20] };
            SeminarPaper sp7 = new SeminarPaper() { Name = "Семинарски 7 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps5, Student = st4, PaperFile = new byte[20] };
            SeminarPaper sp8 = new SeminarPaper() { Name = "Семинарски 8 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps6, Student = st4, PaperFile = new byte[20] };
            SeminarPaper sp9 = new SeminarPaper() { Name = "Семинарски 9 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps6, Student = st5, PaperFile = new byte[20] };
            SeminarPaper sp10 = new SeminarPaper() { Name = "Семинарски 10 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps7, Student = st5, PaperFile = new byte[20] };
            SeminarPaper sp11 = new SeminarPaper() { Name = "Семинарски 11 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps7, Student = st6, PaperFile = new byte[20] };
            SeminarPaper sp12 = new SeminarPaper() { Name = "Семинарски 12 - тест", PublishDate = DateTime.Now, ProfessorSubject = ps8, Student = st6, PaperFile = new byte[20] };
            
            seminarPapers.Add(sp1);
            seminarPapers.Add(sp2);
            seminarPapers.Add(sp3);
            seminarPapers.Add(sp4);
            seminarPapers.Add(sp5);
            seminarPapers.Add(sp6);
            seminarPapers.Add(sp7);
            seminarPapers.Add(sp8);
            seminarPapers.Add(sp9);
            seminarPapers.Add(sp10);
            seminarPapers.Add(sp11);
            seminarPapers.Add(sp12);
        }    
    }
}
