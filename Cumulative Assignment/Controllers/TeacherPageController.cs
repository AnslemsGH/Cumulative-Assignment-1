using Microsoft.AspNetCore.Mvc;
using Cumulative_Assignment.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Cumulative_Assignment.Controllers
{
    public class TeacherPageController : Controller
    {
        private TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<Teacher> Teachers = _api.ListTeacherInfo();

            return View(Teachers);
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            Teacher ATeacher = _api.ATeacherInfo(id);

            return View(ATeacher);
        }
    }
}
