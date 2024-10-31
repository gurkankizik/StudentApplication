using Microsoft.AspNetCore.Mvc;
using StudentApplication.Models;
using System.Diagnostics;

namespace StudentApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDbContext _context; // Assuming you have a DbContext for your application

        public HomeController(ILogger<HomeController> logger, StudentDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Students.ToList(); // Fetch the list of students from the database
            return View(students); // Pass the list to the view
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
    }
}
