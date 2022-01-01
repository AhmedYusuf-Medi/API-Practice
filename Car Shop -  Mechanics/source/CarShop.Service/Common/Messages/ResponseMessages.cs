namespace CarShop.Service.Common.Messages
{
    public static class ResponseMessages
    {
        //Entity
        public const string Entity_Create_Succeed = "{0} was successfully created!";
        public const string Entity_Delete_Succeed = "{0} was successfully deleted!";
        public const string Entity_Edit_Succeed = "{0} was successfully edited!";
        public const string Entity_GetAll_Succeed = "Successfully got all {0}!";
        public const string Entity_Get_Succeed = "Successfully got {0}!";

        //Auth
        public const string Logout_Suceed = "Succsessfully logout";
        public const string Login_Suceed = "Successfully loged in!";
        public const string Check_Email_For_Verification = "Please check your email for verification link.";
        public const string Email_Verification_Succeed = "{0} was verified!";
        public const string Send_Mail_Succeed = "Email was successfully send!";
    }
}