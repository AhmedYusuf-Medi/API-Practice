using System;

namespace CarShop.Models.Base.Common
{
    public static class ValidationConstants
    {
        //Table names
        public const string User_Role = "User_Roles";
        public const string Issue_Priority = "Issue_Priorities";
        public const string Issue_Status = "Issue_Statuses";
        public const string Vehicle_Brand = "Vehicle_Brands";
        public const string Vehicle_Type = "Vehicle_Types";
        public const string Report_Type = "Report_Types";
        //Issue
        public const byte Min_Issue_Description_Length = 5;
        //Report
        public const byte Min_Report_Description_Length = 5;
        public const int Max_Report_Description_Length = 300;
        //Repor Type
        public const byte Min_ReportType_Length = 2;
        public const byte Max_ReportType_Length = 40;
        //Vehicle
        public const int Year_Of_First_Care = 1945;
        public const byte Max_Model_Length = 20;
        public const byte Min_Model_Length = 2;
        public const string Plate_Number_Regex = @"^[A-Z]{2}[0-9]{4}[A-Z]{2}";
        public const string Invalid_Plate_Number = "Must be a valid Plate number 2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters";
        //User
        public const byte Max_Username_Length = 20;
        public const byte Min_Username_Length = 4;
        public const string Password_Regex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
        public const string Invalid_Password_Message = "Password must be at least 8 symbols should contain capital letter, digit and special symbol (+, -, *, &, ^, …)!";
        //Role
        public const byte Max_Role_Length = 50;
        public const byte Min_Role_Length = 5;
        //Status
        public const byte Max_Status_Length = 40;
        public const byte Min_Status_Length = 4;
        //Priority
        public const byte Max_Priority_Length = 40;
        public const byte Min_Priority_Length = 4;
        public const byte Max_Severity_Range = 10;
        public const byte Min_Severity_Range = 1;
        //Vehicle Type
        public const byte Max_Vehicle_Type_Length = 30;
        public const byte Min_Vehicle_Type_Length = 3;
        //Vehicle Brand
        public const byte Max_Vehicle_Brand_Length = 30;
        public const byte Min_Vehicle_Brand_Length = 2;
    }
}