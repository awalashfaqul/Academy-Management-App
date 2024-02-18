using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AcademyManagementApp;

class Program
{
    static string connectionString = "Server=localhost,1433;Database=AcademyDB;User Id=sa;Password=cH@sdotnet23;TrustServerCertificate=true";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Get all students");
            Console.WriteLine("2. Get all students in a certain class");
            Console.WriteLine("3. Add new staff");
            Console.WriteLine("4. Get staff");
            Console.WriteLine("5. Get all grades set in the last month");
            Console.WriteLine("6. Average grade per course");
            Console.WriteLine("7. Add new students");
            Console.WriteLine("8. Add New Class");
            Console.WriteLine("9. Add New Course");
            Console.WriteLine("e. Exit");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        GetAllStudents();
                        break;
                    case 2:
                        GetAllStudentsInClass();
                        break;
                    case 3:
                        AddNewStaff();
                        break;
                    case 4:
                        GetStaff();
                        break;
                    case 5:
                        GetAllGradesLastMonth();
                        break;
                    case 6:
                        AverageGradePerCourse();
                        break;
                    case 7:
                        AddNewStudents();
                        break;
                    case 8:
                        AddNewClass();
                        break;
                    case 9:
                        AddNewCourse();
                        break;
                    case 'e':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void GetAllStudents()
    {
        Console.WriteLine("Select sorting option:");
        Console.WriteLine("1. Sort by first name ascending");
        Console.WriteLine("2. Sort by first name descending");
        Console.WriteLine("3. Sort by last name ascending");
        Console.WriteLine("4. Sort by last name descending");

        if (int.TryParse(Console.ReadLine(), out int sortOption))
        {
            string sortOrder = (sortOption % 2 == 0) ? "DESC" : "ASC";
            string sortBy = (sortOption <= 2) ? "FirstName" : "LastName";

            List<Student> students = GetStudents($"ORDER BY {sortBy} {sortOrder}");

            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentID} - {student.FirstName} {student.LastName}");
            }
        }
        else
        {
            Console.WriteLine("Invalid option. Please try again.");
        }
    }

    static void GetAllStudentsInClass()
    {
        Console.WriteLine("List of Classes:");
        ListClasses();

        Console.WriteLine("Enter the ClassID to view students:");
        if (int.TryParse(Console.ReadLine(), out int classId))
        {
            Console.WriteLine("Select sorting option:");
            Console.WriteLine("1. Sort by first name ascending");
            Console.WriteLine("2. Sort by first name descending");
            Console.WriteLine("3. Sort by last name ascending");
            Console.WriteLine("4. Sort by last name descending");

            if (int.TryParse(Console.ReadLine(), out int sortOption))
            {
                string sortOrder = (sortOption % 2 == 0) ? "DESC" : "ASC";
                string sortBy = (sortOption <= 2) ? "FirstName" : "LastName";

                List<Student> students = GetStudents($"WHERE ClassID = {classId} ORDER BY {sortBy} {sortOrder}");

                Console.WriteLine($"Students in Class {classId}:");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.StudentID} - {student.FirstName} {student.LastName}");
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ClassID. Please try again.");
        }
    }

    static void AddNewStaff()
    {
        Console.WriteLine("Enter staff information:");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Category: ");
        string category = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Staff (FirstName, LastName, Category) " +
                           "VALUES (@FirstName, @LastName, @Category)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@Category", category);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("New staff added successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to add new staff.");
                }
            }
        }
    }

    static void GetStaff()
    {
        Console.WriteLine("Select option:");
        Console.WriteLine("1. View all staff");
        Console.WriteLine("2. View staff by category");

        if (int.TryParse(Console.ReadLine(), out int option))
        {
            string query = "SELECT * FROM Staff";
            if (option == 2)
            {
                Console.Write("Enter category to filter by: ");
                string categoryFilter = Console.ReadLine();
                query = $"SELECT * FROM Staff WHERE Category = '{categoryFilter}'";
            }

            List<Staff> staffMembers = GetStaffMembers(query);

            Console.WriteLine("Staff:");
            foreach (var staff in staffMembers)
            {
                Console.WriteLine($"{staff.StaffID} - {staff.FirstName} {staff.LastName} ({staff.Category})");
            }
        }
        else
        {
            Console.WriteLine("Invalid option. Please try again.");
        }
    }

    static void GetAllGradesLastMonth()
    {
        DateTime lastMonth = DateTime.Now.AddMonths(-1);

        string query = $@"SELECT s.FirstName, s.LastName, c.CourseName, g.GradeValue
                          FROM Grades g
                          JOIN Students s ON g.StudentID = s.StudentID
                          JOIN Courses c ON g.CourseID = c.CourseID
                          WHERE g.DateSet >= '{lastMonth:yyyy-MM-dd}'";

        List<Grade> grades = GetGrades(query);

        Console.WriteLine("Grades set in the last month: ");
        foreach (var grade in grades)
        {
            Console.WriteLine($"{grade.StudentID} - {grade.CourseID} - Grade: {grade.GradeValue}");
        }
    }

    static void AverageGradePerCourse()
    {
        string query = @"SELECT c.CourseName, 
                                AVG(g.GradeValue) AS AverageGrade,
                                MAX(g.GradeValue) AS HighestGrade,
                                MIN(g.GradeValue) AS LowestGrade
                         FROM Grades g
                         JOIN Courses c ON g.CourseID = c.CourseID
                         GROUP BY c.CourseName";

        List<AverageGrade> averageGrades = GetAverageGrades(query);

        Console.WriteLine("Average Grade per Course:");
        foreach (var averageGrade in averageGrades)
        {
            Console.WriteLine($"{averageGrade.CourseName} - Average: {averageGrade.AverageGradeValue}, Highest: {averageGrade.HighestGradeValue}, Lowest: {averageGrade.LowestGradeValue}");
        }
    }

    static void AddNewStudents()
    {
        Console.WriteLine("Enter student information:");
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Date of Birth (yyyy-MM-dd): ");
        DateTime dateOfBirth;
        if (DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
        {
            Console.WriteLine("List of Classes:");
            ListClasses();

            Console.Write("Enter ClassID: ");
            if (int.TryParse(Console.ReadLine(), out int classId))
            {
                Console.Write("Enter CourseID: ");
                if (int.TryParse(Console.ReadLine(), out int courseId))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO Students (FirstName, LastName, DateOfBirth, ClassID, CourseID) " +
                                       "VALUES (@FirstName, @LastName, @DateOfBirth, @ClassID, @CourseID)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", firstName);
                            command.Parameters.AddWithValue("@LastName", lastName);
                            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                            command.Parameters.AddWithValue("@ClassID", classId);
                            command.Parameters.AddWithValue("@CourseID", courseId);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("New student added successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add new student.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid CourseID. Failed to add new student.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ClassID. Failed to add new student.");
            }
        }
        else
        {
            Console.WriteLine("Invalid date format. Failed to add new student.");
        }
    }

    static void AddNewClass()
    {
        Console.WriteLine("Enter the ClassID:");
        if (!int.TryParse(Console.ReadLine(), out int classId))
        {
            Console.WriteLine("Invalid ClassID. Aborting operation.");
            return;
        }

        Console.WriteLine("Enter the ClassName:");
        string className = Console.ReadLine();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Classes (ClassID, ClassName) VALUES (@ClassID, @ClassName)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", classId);
                    command.Parameters.AddWithValue("@ClassName", className);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("New class added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add new class.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    static void ListClasses()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Classes";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["ClassID"]} - {reader["ClassName"]}");
                    }
                }
            }
        }
    }

    static void AddNewCourse()
    {
        Console.WriteLine("Enter the CourseID:");
        if (!int.TryParse(Console.ReadLine(), out int courseId))
        {
            Console.WriteLine("Invalid CourseID. Aborting operation.");
            return;
        }

        Console.WriteLine("Enter the CourseName:");
        string courseName = Console.ReadLine();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Courses (CourseID, CourseName) VALUES (@CourseID, @CourseName)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@CourseName", courseName);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("New course added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to add new course.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    static List<Student> GetStudents(string condition = "")
    {
        List<Student> students = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = $"SELECT * FROM Students {condition}";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            ClassID = (int)reader["ClassID"],
                            CourseID = (int)reader["CourseID"]
                        });
                    }
                }
            }
        }

        return students;
    }

    static List<Staff> GetStaffMembers(string query)
    {
        List<Staff> staffMembers = new List<Staff>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        staffMembers.Add(new Staff
                        {
                            StaffID = (int)reader["StaffID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Category = (string)reader["Category"]
                        });
                    }
                }
            }
        }

        return staffMembers;
    }

    static List<Grade> GetGrades(string query)
    {
        List<Grade> grades = new List<Grade>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        grades.Add(new Grade
                        {
                            StudentID = (string)reader["StudentID"],
                            CourseID = (string)reader["CourseID"],
                            GradeValue = (int)reader["GradeValue"],
                            graDe = (string)reader["graDe"]
                        });
                    }
                }
            }
        }

        return grades;
    }

    static List<AverageGrade> GetAverageGrades(string query)
    {
        List<AverageGrade> averageGrades = new List<AverageGrade>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        averageGrades.Add(new AverageGrade
                        {
                            CourseName = (string)reader["CourseName"],
                            AverageGradeValue = (double)reader["AverageGrade"],
                            HighestGradeValue = (int)reader["HighestGrade"],
                            LowestGradeValue = (int)reader["LowestGrade"]
                        });
                    }
                }
            }
        }

        return averageGrades;
    }
}