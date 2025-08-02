using System.Collections.Generic;
using Cumulative_Assignment.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySql.Data.MySqlClient;
//using MySqlX.ResultSet;


namespace Cumulative_Assignment.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        // Setting up 
        private SchoolDbContext _context = new SchoolDbContext();

        //private readonly SchoolDbContext _context;

        //public TeacherAPIController(SchoolDbContext context)
        //{
        //    _context = context;
        //}

        /// <summary>
        /// Below API request will fetch the list of all teachers from the database School and return in a json format as stored in the list variable called TeacherInfo. It also has an optional search feature which allows user to filter the output.
        /// </summary>
        /// <returns>
        /// It will return all the records from the Teachers table in school DB along with their corresponding column name individually in an array/list unless searched in which case it will return only the output that matches the criteria.
        /// </returns>
        /// <example>
        /// GET: api/teacher/ListTeacherInfo > [{
        /// [{
          //  "teacherId": 1,
          //  "teacherFName": "Alexander",
          //  "teacherLName": "Bennett",
          //  "employeeNumber": "T378",
          //  "hireDate": "2016-08-05T00:00:00",
          //  "salary": 55.3
          //},
          //{
          //  "teacherId": 2,
          //  "teacherFName": "Caitlin",
          //  "teacherLName": "Cummings",
          //  "employeeNumber": "T381",
          //  "hireDate": "2014-06-10T00:00:00",
          //  "salary": 62.77
          //}]
        /// </example>



        [HttpGet(template: "ListTeacherInfo")]

        public List<Teacher> ListTeacherInfo(string? SearchKey)
        {    //creating a list to store info of ALL teachers
            List<Teacher> TeacherInfo = new List<Teacher>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                //Opening the database for use.
                Connection.Open();

                //Query to be run on the database.
                string query = "select * from teachers where teacherfname like @key or teacherlname like @key";

                //Creating a command
                MySqlCommand Command = Connection.CreateCommand(); ;

                Command.CommandText = query;
                Command.Parameters.AddWithValue("@key", "%"+SearchKey+"%");
                Command.Prepare();

                //Executing the command
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Using while loop to run through each row of the table and store the output in the list.
                    while (ResultSet.Read())
                    {
                        ///Tried this earlier based on the recording, it gives an output but not in a structured format.
                        //int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        //string TeacherFName = ResultSet["teacherfname"].ToString();
                        //string TeacherLName = ResultSet["teacherlname"].ToString();
                        //Decimal Salary = Decimal.Parse(ResultSet["salary"].ToString());
                        //DateTime HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        //string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        //TeacherInfo.Add(TeacherId + TeacherFName + TeacherLName + Salary + HireDate + EmployeeNumber);
                        ////TeacherInfo.Add(EmployeeNumber);

                        //Creating a variable and storing the output.
                        Teacher Teacherinfo = new Teacher();

                        //Fetching details from the db and storing it in the variable.
                        Teacherinfo.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        Teacherinfo.TeacherFName = ResultSet["teacherfname"].ToString();
                        Teacherinfo.TeacherLName = ResultSet["teacherlname"].ToString();
                        Teacherinfo.Salary = Decimal.Parse(ResultSet["salary"].ToString());
                        Teacherinfo.HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                        Teacherinfo.EmployeeNumber = ResultSet["employeenumber"].ToString();

                        //Adding the individual teacher details into the array/list created above.
                        TeacherInfo.Add(Teacherinfo);

                    }
                }
            }
            //outputing the db output
            return TeacherInfo;
        }


        /// <summary>
        /// This block of code allows user to see results for a specific teacher
        /// </summary>
        /// <param name="teachersid">The teachers are uniquely identified by their id so we use this as a parameter to filter and display output for specific teacher</param>
        /// <returns>
        /// The output is all the fields from the teachers table in the DB but only for the specific teacher who was requested in the parameter.
        /// </returns>
        /// <example> /api/teacher/ATeacherInfo/5 >>
        /// {
        //  "teacherId": 5,
        //  "teacherFName": "Jessica",
        //  "teacherLName": "Morris",
        //  "employeeNumber": "T389",
        //  "hireDate": "2012-06-04T00:00:00",
        //  "salary": 48.62
        //}
        /// </example>

        [HttpGet(template: "ATeacherInfo/{teachersid}")]

        public Teacher ATeacherInfo(int teachersid)
        {
            //Created a variable to store the output from query in a required format.
            Teacher TeacherInfo = new Teacher();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                //Opening the db connection
                Connection.Open();

                //Query that return all the fields for a particular teacher whose id is considered as an input parameter.
                string query = "select * from teachers where teacherid = @id";

                //Creating a command.
                MySqlCommand Command = Connection.CreateCommand(); ;

                Command.CommandText = query;
                Command.Parameters.AddWithValue("@id",teachersid);

                //Executing the command
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
                ///Commented because I am getting error saying cannot convert string because of which I cant run my code and I am unable to understand why.
                //    else
                //    {
                //        string message = "TeacherId input is incorrect as no teacher available is linked to the id.";
                //        return message;
                //    }
                }

                //returning the output.
                return TeacherInfo;
            }

        }

        /// <summary>
        /// Adding a new teacher into the database.
        /// </summary>
        /// <param name="NewTeacher">New teacher is a set of data that will be input into the database, that user will be inputting on the UI</param>
        /// <returns>This code return the new teacher id that was created based on the input in the database</returns>
        /// <example>
        /// POST: https://localhost:7059/TeacherPage/New
        /// First Name: Anslem
        /// Last Name: Coelho
        /// Employee Number: A1234
        /// Hire Date: 08-01-2025
        /// Salary: 75
        /// </example>

        [HttpPost(template:"AddTeacher")]
        
        public int AddTeacher([FromBody]Teacher NewTeacher)
        {
            string query = "insert into teachers (teacherfname, teacherlname, employeenumber,hiredate,salary) values (@fname, @lname, @num, @hiredate, @sal);";

            int TeacherId = -1;
            using (MySqlConnection Conn = _context.AccessDatabase())
            {
                Conn.Open();

                MySqlCommand Command = Conn.CreateCommand();
                Command.CommandText = query;
                Command.Parameters.AddWithValue("@fname", NewTeacher.TeacherFName);
                Command.Parameters.AddWithValue("@lname", NewTeacher.TeacherLName);
                Command.Parameters.AddWithValue("@num", NewTeacher.EmployeeNumber);
                Command.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
                Command.Parameters.AddWithValue("@sal", NewTeacher.Salary);

                Command.ExecuteNonQuery();
                TeacherId = Convert.ToInt32(Command.LastInsertedId);
            }

            return TeacherId;
        }


        /// <summary>
        /// This code deletes the teacher if matching the id received from the database
        /// </summary>
        /// <param name="id">The primary Key of TeacherID</param>
        /// <returns>The number of rows affected by delete</returns>
        /// <example>
        /// DELETE api/TeacherAPI/DeleteTeacher/11 -> 1
        /// </example>
        [HttpDelete(template:"DeleteTeacher/{id}")]
        public int DeleteTeacher(int id)
        {
            string query = "delete from teachers where teacherid=@id";
            int RowsAffected = 0;

            using (MySqlConnection Conn = _context.AccessDatabase())
            {
                Conn.Open();

                MySqlCommand Command = Conn.CreateCommand();
                Command.CommandText = query;
                Command.Parameters.AddWithValue("@id", id);

                RowsAffected = Command.ExecuteNonQuery();
                
            }

            return RowsAffected;
        }

    }

}
