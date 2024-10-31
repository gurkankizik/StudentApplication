using Microsoft.EntityFrameworkCore;

namespace StudentApplication.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Grade> Grades { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Model configuration for Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Class).IsRequired().HasMaxLength(50);

                entity.HasMany(e => e.Grades)
                      .WithOne()
                      .HasForeignKey(g => g.StudentId);

                // Seed data for Student entity
                entity.HasData(new Student
                {
                    Id = 1,
                    Name = "Gürkan",
                    Surname = "Kızık",
                    Age = 22,
                    Department = "Computer Science",
                    Class = "CS101"
                });
            });

            // Model configuration for Grade entity
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CourseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Score).IsRequired();

                // Seed data for Grade entity
                entity.HasData(
                    new Grade { Id = 1, StudentId = 1, CourseName = "Math", Score = 85 },
                    new Grade { Id = 2, StudentId = 1, CourseName = "Science", Score = 90 }
                );
            });
        }
    }
}
