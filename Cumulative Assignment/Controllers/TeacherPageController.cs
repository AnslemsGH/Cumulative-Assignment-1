using Microsoft.AspNetCore.Mvc;
using Cumulative_Assignment.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Cumulative_Assignment.Controllers
{
    public class TeacherPageController : Controller
    {
        //Dependency Injection
        private TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        //Creating output for all teachers as a list in link format fetched from API
        [HttpGet]
        public IActionResult List()
        {
            //Storing the output from the API in a Variable
            List<Teacher> Teachers = _api.ListTeacherInfo();

            //Outputting the variable in the view i.e. html
            return View(Teachers);
        }


        //Fetching result for individual teacher based on the input parameter
        //example: api/TeacherPage/Show/7 >  Alexander Farias 
        //                                   Teacher Id: 7
        //                                   Hired On : 12-12-2006 12:00:00 
        //                                   Salary(in dollars): 55.8 
        [HttpGet]
        public IActionResult Show(int id)
        {
            //Storing the output from the API in a Variable
            Teacher ATeacher = _api.ATeacherInfo(id);

            //Outputting the variable in the view i.e. html
            return View(ATeacher);
        }
    }
}
