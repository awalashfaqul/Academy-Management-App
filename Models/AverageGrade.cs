using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AcademyManagementApp.Models
{
	public class AverageGrade
	{
        public string CourseName { get; set; }
        public double AverageGradeValue { get; set; }
        public int HighestGradeValue { get; set; }
        public int LowestGradeValue { get; set; }
    }
}

