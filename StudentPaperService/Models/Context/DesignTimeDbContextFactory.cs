using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPaperService.Models.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudentPaperServiceContext>
    {
        public StudentPaperServiceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentPaperServiceContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SymorgApplication;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            StudentPaperServiceContext context = new StudentPaperServiceContext(optionsBuilder.Options);

            if (!context.AllMigrationsApplied())
            {
                context.Database.Migrate();
                context.EnsureDatabaseSeeded();
            }

            return context;
        }
    }
}
