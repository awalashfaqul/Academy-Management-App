using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AcademyManagementApp
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassID { get; set; }
        public int CourseID { get; set; }
    }
}


