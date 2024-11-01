using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApplication.Models;
using System.Diagnostics;

namespace StudentApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDbContext _context;

        public HomeController(ILogger<HomeController> logger, StudentDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string sortOrder, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SurnameSortParm"] = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewData["FacultySortParm"] = sortOrder == "Faculty" ? "faculty_desc" : "Faculty";
            ViewData["DepartmentSortParm"] = sortOrder == "Department" ? "department_desc" : "Department";
            ViewData["StudentNumberSortParm"] = sortOrder == "StudentNumber" ? "studentNumber_desc" : "StudentNumber";
            ViewData["GPASortParm"] = sortOrder == "GPA" ? "gpa_desc" : "GPA";
            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Students
                           select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString) || s.Surname.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Surname":
                    students = students.OrderBy(s => s.Surname);
                    break;
                case "surname_desc":
                    students = students.OrderByDescending(s => s.Surname);
                    break;
                case "Faculty":
                    students = students.OrderBy(s => s.Faculty);
                    break;
                case "faculty_desc":
                    students = students.OrderByDescending(s => s.Faculty);
                    break;
                case "Department":
                    students = students.OrderBy(s => s.Department);
                    break;
                case "department_desc":
                    students = students.OrderByDescending(s => s.Department);
                    break;
                case "StudentNumber":
                    students = students.OrderBy(s => s.StudentNumber);
                    break;
                case "studentNumber_desc":
                    students = students.OrderByDescending(s => s.StudentNumber);
                    break;
                case "GPA":
                    students = students.OrderBy(s => s.GPA);
                    break;
                case "gpa_desc":
                    students = students.OrderByDescending(s => s.GPA);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            var paginatedList = await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize);

            ViewData["TotalCount"] = await _context.Students.CountAsync();

            return View(paginatedList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(s => s.Grades)
                .FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
