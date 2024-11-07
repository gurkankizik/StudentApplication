using System.ComponentModel.DataAnnotations;

namespace StudentApplication.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters")]
        public required string Surname { get; set; }

        [Required(ErrorMessage = "Faculty is required")]
        [StringLength(100, ErrorMessage = "Faculty cannot be longer than 100 characters")]
        public required string Faculty { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [StringLength(100, ErrorMessage = "Department cannot be longer than 100 characters")]
        public required string Department { get; set; }

        [Required(ErrorMessage = "Student Number is required")]
        [StringLength(20, ErrorMessage = "Student Number cannot be longer than 20 characters")]
        public required string StudentNumber { get; set; }

        public double GPA { get; set; }

        public List<Grade> Grades { get; set; } = new();

        public bool IsDeleted { get; set; } = false;
    }
}
