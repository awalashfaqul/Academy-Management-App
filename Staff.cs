﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AcademyManagementApp
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Category { get; set; }
    }
}

