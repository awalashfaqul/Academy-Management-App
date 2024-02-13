using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AcademyManagementApp.Models
{
    public class Grade
    {
        public int GradeID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public int GradeValue { get; set; }
    }
}

