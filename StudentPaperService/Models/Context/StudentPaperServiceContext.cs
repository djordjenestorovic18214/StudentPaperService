using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentPaperService.Models.Context
{
    public class StudentPaperServiceContext : IdentityDbContext<ApplicationUser>
    {
        public StudentPaperServiceContext(DbContextOptions<StudentPaperServiceContext> options) : base(options)
        { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SeminarPaper> SeminarPapers { get; set; }
        public DbSet<FinalPaper> FinalPapers { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }
        public DbSet<FinalPaperType> FinalPaperTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentPaperServiceDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            options.UseSqlServer(connectionString);
            //options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<SeminarPaper>().ToTable("SeminarPapers");
            modelBuilder.Entity<FinalPaper>().ToTable("FinalPapers");
            modelBuilder.Entity<Professor>().ToTable("Professors");
            modelBuilder.Entity<ProfessorSubject>().ToTable("ProfessorSubjects");

            #region StudentTableConstraint

            modelBuilder.Entity<Student>()
                .Property(s => s.IndexNumber)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.IndexNumber)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(s => s.SeminarPapers)
                .WithOne(s => s.Student);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.FinalPapers)
                .WithOne(s => s.Student);
            #endregion

            #region ProfessorTableConstraint

            modelBuilder.Entity<Professor>()
                .Property(p => p.FirstName)
                .IsRequired();

            modelBuilder.Entity<Professor>()
                .Property(p => p.LastName)
                .IsRequired();

            modelBuilder.Entity<Professor>()
                .HasMany(s => s.FinalPapers)
                .WithOne(s => s.Mentor);

            #endregion

            #region SubjectTableConstraint

            modelBuilder.Entity<Subject>()
                .Property(s => s.SubjectId)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .Property(s => s.Name)
                .IsRequired();

            #endregion

            #region ProfessorSubjectTableConstraint

            modelBuilder.Entity<ProfessorSubject>()
               .HasKey(ps => new { ps.ProfessorId, ps.SubjectId });

            modelBuilder.Entity<ProfessorSubject>()
                .HasOne<Professor>(ps => ps.Professor)
                .WithMany(ps => ps.ProfessorSubjects)
                .HasForeignKey(ps => ps.ProfessorId);


            modelBuilder.Entity<ProfessorSubject>()
                .HasOne<Subject>(ps => ps.Subject)
                .WithMany(ps => ps.ProfessorSubjects)
                .HasForeignKey(ps => ps.SubjectId);

            modelBuilder.Entity<ProfessorSubject>()
                .HasMany(s => s.SeminarPapers)
                .WithOne(s => s.ProfessorSubject);

            #endregion

            #region SeminarPaperTableConstraint

            modelBuilder.Entity<SeminarPaper>()
                .Property(p => p.SeminarPaperId)
                .IsRequired();

            modelBuilder.Entity<SeminarPaper>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<SeminarPaper>()
                .Property(p => p.PublishDate)
                .IsRequired();

            modelBuilder.Entity<SeminarPaper>()
                .Property(p => p.PaperFile)
                .IsRequired();

            modelBuilder.Entity<SeminarPaper>()
                .Property(p => p.ProfessorSubjectId)
                .IsRequired();

            modelBuilder.Entity<SeminarPaper>()
                .HasOne(p => p.ProfessorSubject)
                .WithMany(p => p.SeminarPapers);

            modelBuilder.Entity<SeminarPaper>()
                .Property(p => p.StudentId)
                .IsRequired();

            modelBuilder.Entity<SeminarPaper>()
                .HasOne(p => p.Student)
                .WithMany(p => p.SeminarPapers);

            #endregion

            #region FinalPaperTableConstraint

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.FinalPaperId)
                .IsRequired();

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.PublishDate)
                .IsRequired();

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.PaperFile)
                .IsRequired();

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.StudentId)
                .IsRequired();

            modelBuilder.Entity<FinalPaper>()
                .HasOne(p => p.Student)
                .WithMany(p => p.FinalPapers);

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.MentorId)
                .IsRequired();

            modelBuilder.Entity<FinalPaper>()
                .HasOne(p => p.Mentor)
                .WithMany(p => p.FinalPapers);

            modelBuilder.Entity<FinalPaper>()
                .Property(p => p.FinalPaperTypeId)
                .IsRequired();

            #endregion

            #region FinalPaperTypeTableConstraint

            modelBuilder.Entity<FinalPaperType>()
                .Property(p => p.FinalPaperTypeId)
                .IsRequired();

            modelBuilder.Entity<FinalPaperType>()
                .Property(p => p.Type)
                .IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }

}
