using Cumulative_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
//using MySqlX.ResultSet;


namespace Cumulative_Assignment.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {

        private SchoolDbContext _context = new SchoolDbContext();

        //private readonly SchoolDbContext _context;

        //public TeacherAPIController(SchoolDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet(template: "ListTeacherInfo")]

        public List<Teacher> ListTeacherInfo()
        {    //creating a list of teachers
            List<Teacher> TeacherInfo = new List<Teacher>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string query = "select * from teachers";

                MySqlCommand Command = Connection.CreateCommand(); ;

                Command.CommandText = query;

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        //int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        //string TeacherFName = ResultSet["teacherfname"].ToString();
                        //string TeacherLName = ResultSet["teacherlname"].ToString();
                        //Decimal Salary = Decimal.Parse(ResultSet["salary"].ToString());
                        //DateTime HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        //string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        //TeacherInfo.Add(TeacherId + TeacherFName + TeacherLName + Salary + HireDate + EmployeeNumber);
                        ////TeacherInfo.Add(EmployeeNumber);


                        Teacher Teacherinfo = new Teacher();
                        Teacherinfo.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        Teacherinfo.TeacherFName = ResultSet["teacherfname"].ToString();
                        Teacherinfo.TeacherLName = ResultSet["teacherlname"].ToString();
                        Teacherinfo.Salary = Decimal.Parse(ResultSet["salary"].ToString());
                        Teacherinfo.HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        Teacherinfo.EmployeeNumber = ResultSet["employeenumber"].ToString();

                        TeacherInfo.Add(Teacherinfo);

                    }
                }
            }
            return TeacherInfo;
        }



        [HttpGet(template: "ATeacherInfo/{teachersid}")]

        public Teacher ATeacherInfo(int teachersid)
        {
            Teacher TeacherInfo = new Teacher();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                string query = $"select * from teachers where teacherid = {teachersid}";

                MySqlCommand Command = Connection.CreateCommand(); ;

                Command.CommandText = query;

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    if (ResultSet.Read())
                    {
                        //Teacher Teacherinfo = new Teacher();
                        TeacherInfo.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        TeacherInfo.TeacherFName = ResultSet["teacherfname"].ToString();
                        TeacherInfo.TeacherLName = ResultSet["teacherlname"].ToString();
                        TeacherInfo.Salary = Decimal.Parse(ResultSet["salary"].ToString());
                        TeacherInfo.HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        TeacherInfo.EmployeeNumber = ResultSet["employeenumber"].ToString();

                    }
                    //I am getting error saying cant convert string and I am unable to understand why.
                //    else
                //    {
                //        string message = "TeacherId input is incorrect as no teacher available is linked to the id.";
                //        return message;
                //    }
                }

                return TeacherInfo;
            }

        }


    }

}
