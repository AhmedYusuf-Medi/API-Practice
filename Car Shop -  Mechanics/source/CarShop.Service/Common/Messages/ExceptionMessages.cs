namespace CarShop.Service.Common.Messages
{
    public static class ExceptionMessages
    {
        //Send grid
        public const string Invalid_Email_Arguments = "Subject and message should be provided.";
        public const string Invalid_Operation_SendGrid = "Mail sender failed twice!";

        //Cloudinary
        public const string Invalid_Operation_Cloudinary = "Uploading profile picture failed twice!";

        //Exception messages that works with entities
        public const string DOESNT_EXIST = "Doesn't exist such a {0}";
        public const string Cannot_Delete_HasRelations = "{0} has {1} cannot delete it!";
        public const string No_Entities = "No entities in collection";
        public const string Already_Reported = "Cannot report {0} more than once!";

        //Exception messages that works with User/Authentication
        public const string Unauthorized = "Invalid authentication credentials for the requested!";
        public const string Cannot_Report_YourSelf = "It is not possible to report your self!";
        public const string User_Not_Found = "Not exist such user!";
        public const string Invalid_Email = "Use correct e-mail for login!";
        public const string Already_Exist = "{0} already exist";
        public const string Invalid_Password = "Invalid password!";
        public const string Invalid_Credentials = "You do not have the enough permission for the operation!";
        public const string Already_Verified = "Already verified!";

        //User service
        public const string No_Roles = "User have no roles!";
        public const string Already_Blocked = "User is already blocked!";
        public const string User_Is_Not_Blocked = "Choosen user is not even blocked!";
    }
}