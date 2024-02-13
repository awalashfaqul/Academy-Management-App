using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using AcademyManagementApp.Functions;
//using AcademyManagementApp.Data;

namespace AcademyManagementApp;
class Program
{
    static string connectionString = "Server=localhost,1433;Database=AcademyDB;User Id=sa;Password=cH@sdotnet23;TrustServerCertificate=true";

    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");

        startAcademyApp.startApp();

        
    }
}

