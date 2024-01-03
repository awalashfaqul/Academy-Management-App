using System;
namespace AcademyManagementApp.Functions
{
	public class startAcademyApp
	{
		public static void startApp()
		{
            while (true)
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
                    Console.WriteLine("8. Exit");

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
	}
}

