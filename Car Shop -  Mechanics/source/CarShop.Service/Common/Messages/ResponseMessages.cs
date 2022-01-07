namespace CarShop.Service.Common.Messages
{
    public static class ResponseMessages
    {
        //Entity
        public const string Entity_Create_Succeed = "{0} was successfully created!";
        public const string Entity_Delete_Succeed = "{0} was successfully deleted!";
        public const string Entity_Edit_Succeed = "{0} was successfully edited!";
        public const string Entity_Partial_Edit_Succeed = "{0} was updated where the passed arguments are valid!";
        public const string Entity_GetAll_Succeed = "Successfully got all {0}!";
        public const string Entity_Get_Succeed = "Successfully got {0}!";
        public const string Entity_Filter_Succeed = "Successfully filtered {0}!";
        public const string Entity_Sort_Succeed = "Successfully sorted {0}!";
        public const string Entity_Property_Is_Taken = "{0}: {1} is already taken!";
        public const string Entity_Partial_Update_Suceed = "{0} was successfully partial updated!";

        //User
        public const string User_Block_Succeed = "Successfully blocked {0}!";
        public const string User_UnBlock_Succeed = "Successfully Unblocked {0}!";
        public const string Role_Register_Succeed = "Successfully assigned {0}!";

        //Auth
        public const string Logout_Succeed = "Succsessfully logout!";
        public const string Login_Succeed = "Successfully loged in!";
        public const string Check_Email_For_Verification = "Please check your email for verification link.";
        public const string Email_Verification_Succeed = "{0} was verified!";
        public const string Send_Mail_Succeed = "Email was successfully send!";

        //Exception
        public const string Exception_Checked = "{0} was succesfully checked!";
    }
}