namespace Cumulative_Assignment.Models
{
    //Creating a model to store the database output in a format.
    public class Teacher
    {
        //Primary Key from the table to uniquely identify a teacher.
        public int TeacherId { get; set; }

        //First Name of the teacher
        public string TeacherFName { get; set; }

        //Last Name of the teacher
        public string TeacherLName { get; set; }

        //Employee Number of the teacher
        public string EmployeeNumber { get; set; }

        //Hire Date of the teacher
        public DateTime HireDate { get; set; }

        //Salary of the teacher
        public decimal Salary { get; set; }
    }
}
