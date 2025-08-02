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

        //Creating output with search parameter for all teachers as a list and individual entry matching the optional seach input key in link format fetched from API
        [HttpGet]
        public IActionResult List(string SearchKey)
        {
            //Storing the output from the API in a Variable
            List<Teacher> Teachers = _api.ListTeacherInfo(SearchKey);

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


        //GET : TeacherPage/New -> A webpage that asks user for the new teacher info
        [HttpGet]

        public IActionResult New()
        {
            return View();
        }


        //POST: TeacherPage/Create -> Storing the values received into the DB.
        [HttpPost]

        public IActionResult Create(string TeacherFName, string TeacherLName, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            int TeacherId = _api.AddTeacher(NewTeacher);


            //Redirecting to the teachers page that was just added.
            return RedirectToAction("Show", new {id = TeacherId});
        }

        //GET: /TeacherPage/DeleteConfirm/{id} -> A webpage that asks a user if they want to delete this article
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.ATeacherInfo(id);
            
            return View(SelectedTeacher);
        }

        //POST: /TeacherPage/Delete/{id} -> Deletes the article and returns to the List.cshtml

        [HttpPost]

        public IActionResult Delete(int id)
        {
            //_api.DeleteTeacher(id);
            
            return RedirectToAction("List");
        }
    }
}
