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

        [HttpGet(template:"ListTeacherInfo")]

        public List<string> ListTeacherInfo()
        {
            List <string> TeacherInfo = new List<string>();

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
                        int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        string TeacherFName = ResultSet["teacherfname"].ToString();
                        string TeacherLName = ResultSet["teacherlname"].ToString();
                        Decimal Salary = Decimal.Parse(ResultSet["salary"].ToString());
                        DateTime HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        TeacherInfo.Add(TeacherId + TeacherFName + TeacherLName + Salary + HireDate + EmployeeNumber);
                        ////TeacherInfo.Add(EmployeeNumber);
                        
                    }
                }
            }
            return TeacherInfo;
        }


 
    }

}
