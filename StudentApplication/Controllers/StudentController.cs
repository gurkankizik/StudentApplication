using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApplication.Data;
using StudentApplication.Models;
using System.Diagnostics;

namespace StudentApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;
        private readonly ExportService _exportService;

        public StudentController(StudentDbContext context, ExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
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
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }
            return View(student);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost("SaveGrade")]
        public async Task<IActionResult> SaveGrade(int studentId, int id, string courseName, int score)
        {
            if (ModelState.IsValid)
            {
                Grade grade;
                if (id == 0)
                {
                    grade = new Grade { StudentId = studentId, CourseName = courseName, Score = score };
                    _context.Grades.Add(grade);
                }
                else
                {
                    grade = await _context.Grades.FindAsync(id);
                    if (grade == null)
                    {
                        return NotFound();
                    }
                    grade.CourseName = courseName;
                    grade.Score = score;
                    _context.Grades.Update(grade);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = studentId });
            }
            return View();
        }

        [HttpPost("DeleteGrade")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = grade.StudentId });
        }
        [HttpGet("ExportStudentsToExcel")]
        public IActionResult ExportStudentsToExcel()
        {
            var content = _exportService.ExportStudentsToExcel();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students.xlsx");
        }
    }
}
