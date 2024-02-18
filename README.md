## To Do

# SSMS - SQL Query
Create a simple database that stores the school informaion, such as -

- [ ] Students
- [ ] Classes that students can belong to
- [ ] Courses
- [ ] Grades received by a particular student in a particular course (store as numbers)
- [ ] Staffs who may belong to different categories (e.g. teacher, administrator, principal)

# Console Application
Create the basis of application

- [ ] Create a new console program in C#
- [ ] Create a simple navigation in the program that allows the user to choose between the functions below. Creating the following functions that the user can navigate to in the application. All these functions are solved through entity framework to get access to database on call for data.

1. **Get all students**
    
     The user can choose whether they want to see the students sorted by first or last name and whether it should be sorted in ascending or descending order.
    
2. **Get all students in a certain class**
    
     The user must first see a list of all classes that exist, then the user can select one of the classes and then all the students in that class will be printed.
    
     Extra challenge: let the user also choose the sorting of the students as in the point above.
    
3. **Add new staff**
    
     The user must be able to enter information about a new employee and that data is then saved in the database.
    
4. **Get staff**
    
     The user can choose whether he wants to see all employees, or only within one of the categories, such as teachers.
    
5. **Get all grades set in the last month**
    
     Here, the user can see a list of all the grades set in the last month, where the student's name, the name of the course and the grade appear.
    
6. **Average grade per course**
    
     Get a list of all courses and the average grade that students received in that course, as well as the highest and lowest grade that someone received in the course.
    
     Here the user can see a list of all courses in the database, the average grade and the highest and lowest grade for each course.
    
     Tip: It can be difficult to do this with grades in the form of letters so you can choose to store the grades as numbers instead.
    
7. **Add new students**
    
     The user gets the opportunity to enter information about a new student and that data is then saved in the database.


- [ ] When a function has been executed, the user must be able to click enter and return to the main menu again. A simple case sentence is sufficient and the user enters a number to navigate to a certain function.



### Academy Management Application

#### Overview
This project is an Academy Management Application implemented in C# that allows managing students, staff, classes, courses, and grades. It provides various functionalities such as adding new staff, students, classes, courses, retrieving student information, and calculating average grades per course.

#### Methods Used
1. **Console Application**: The application is developed as a console application, providing a text-based interface for user interaction.
   
2. **Object-Oriented Programming (OOP)**: The codebase follows OOP principles by defining classes for entities like `Student`, `Staff`, `Class`, `Course`, `Grade`, and `AverageGrade`. This approach enhances code readability, modularity, and maintainability.

3. **ADO.NET (ActiveX Data Objects)**: ADO.NET is used for database connectivity and manipulation. SqlConnection, SqlCommand, and SqlDataReader classes are utilized for executing SQL queries, retrieving data, and interacting with the SQL Server database.

4. **Parameterized Queries**: Parameterized queries are employed to prevent SQL injection attacks and ensure data integrity. This method binds user input securely to SQL query parameters, reducing the risk of malicious SQL injection.

#### Why These Methods?
1. **Console Application**: A console application is chosen for simplicity and ease of use. It provides a straightforward interface suitable for this type of application without the need for complex graphical user interfaces.

2. **Object-Oriented Programming**: OOP promotes code organization, reusability, and extensibility. By structuring the codebase into classes representing real-world entities, it becomes easier to understand, maintain, and scale the application.

3. **ADO.NET for Database Interaction**: ADO.NET is a mature and widely-used technology for database access in .NET applications. It provides efficient data access capabilities and seamless integration with SQL Server, making it a suitable choice for this application's database operations.

4. **Parameterized Queries**: Parameterized queries are considered a best practice for database interactions due to their ability to mitigate SQL injection vulnerabilities. By using parameterized queries, the application ensures secure database operations and protects against malicious attacks.

#### Alternatives
1. **ORM (Object-Relational Mapping)**: Instead of using raw ADO.NET for database access, an ORM framework like Entity Framework could be employed. Entity Framework abstracts away much of the database interaction complexity and offers higher-level abstractions, simplifying data access code. However, for simpler applications like this, using ADO.NET directly provides more control and may be more lightweight.

2. **Web Application**: Instead of a console application, a web-based application could be developed using ASP.NET Core MVC or Blazor. This would provide a more interactive and visually appealing user interface, suitable for applications requiring frequent user interaction or accessibility over the internet.

3. **Stored Procedures**: Instead of inline SQL queries, stored procedures could be used for database operations. Stored procedures offer better performance, security, and encapsulation of database logic. However, they may add complexity to the application and require additional maintenance.

#### Conclusion
The chosen methods prioritize simplicity, security, and maintainability. By leveraging console-based interaction, OOP principles, ADO.NET for database access, and parameterized queries, the application provides a robust solution for managing academy-related data. While alternative approaches exist, the selected methods align well with the requirements and constraints of the project.
