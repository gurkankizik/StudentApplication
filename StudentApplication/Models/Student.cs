namespace StudentApplication.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string StudentNumber { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
