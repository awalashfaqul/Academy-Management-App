using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AcademyManagementApp.Functions
{
	public class viewStudents
	{
        /* Defining function to interact with database to show student list */

        static void ViewStudents()
        {
            using (var context = new AcademyDbContext())
            {
                var students = context.Student.ToList();
                Console.WriteLine("List of Students: ");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.StudentID} - {student.FirstName} {student.LastName}");
                }
            }
        }
    }
}

