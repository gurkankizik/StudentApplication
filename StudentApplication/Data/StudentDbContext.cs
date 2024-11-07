using Microsoft.EntityFrameworkCore;
using StudentApplication.Models;

namespace StudentApplication.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Faculty).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
                entity.Property(e => e.StudentNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.GPA).IsRequired();

                entity.HasMany(e => e.Grades)
                      .WithOne()
                      .HasForeignKey(g => g.StudentId);

                entity.HasData(new Student
                {
                    Id = 1,
                    Name = "Gürkan",
                    Surname = "Kızık",
                    Faculty = "Engineering",
                    Department = "Computer Engineering",
                    StudentNumber = "202103001099",
                    GPA = 2.80
                });
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CourseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Score).IsRequired();

                entity.HasData(
                    new Grade { Id = 1, StudentId = 1, CourseName = "Math", Score = 85 },
                    new Grade { Id = 2, StudentId = 1, CourseName = "Science", Score = 90 }
                );
            });
        }
    }
}
