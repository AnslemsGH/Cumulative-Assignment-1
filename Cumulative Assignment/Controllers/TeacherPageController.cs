using Microsoft.AspNetCore.Mvc;

namespace Cumulative_Assignment.Controllers
{
    public class TeacherPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
