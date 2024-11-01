using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentApplication.Data;
using StudentApplication.Models;

namespace GenerateStudents
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<StudentDbContext>(options =>
                    options.UseSqlServer("Server=.;Database=StudentDb;Trusted_Connection=True;TrustServerCertificate=True"))
                .BuildServiceProvider();

            using (var context = serviceProvider.GetService<StudentDbContext>())
            {
                var random = new Random();
                var faculties = new[] { "Engineering", "Arts", "Science", "Business" };
                var departments = new[] { "Computer Science", "Mechanical Engineering", "Physics", "Marketing" };

                for (int i = 1; i <= 150; i++)
                {
                    var student = new Student
                    {
                        Name = $"StudentName{i}",
                        Surname = $"StudentSurname{i}",
                        Faculty = faculties[random.Next(faculties.Length)],
                        Department = departments[random.Next(departments.Length)],
                        StudentNumber = $"S{i:00000}",
                        GPA = Math.Round(random.NextDouble() * 4, 2)
                    };

                    context.Students.Add(student);
                }

                context.SaveChanges();
            }

            Console.WriteLine("150 students have been generated and inserted into the database.");
        }
    }
}