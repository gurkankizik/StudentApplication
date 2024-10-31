namespace StudentApplication.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public string Class { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
