namespace CarShop.Service.Common.Base
{
    public static class ExternalProviders
    {
        //Mail inf
        public const string Abv_Account = "carshop-mechanics@abv.bg";
        public const string Mail_Password = "passwordQ1!";
        public const string SMTP_Server = "smtp.abv.bg";
        public const int SMTP_Port = 465;
        //Sender
        public const string Sender_Name = "Car Shop";
        public const string SendGrid_ApiKey = "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";
        //Cloudinary settings
        public const string Cloudinary_Name = "diihcd5cx";
        public const string Cloudinary_Key = "653456971911318";
        public const string Cloudinary_Secret = "0F-OawIxly_YApVbCfbyk9-IUgE";
        //Sendgrid arguments
        public const string SendGrid_ComfirmMail = "Car shop comfirm email verification flow.";
        public const string SendGrid_LinkForVerification = "http://localhost:1920/api/Accounts/verification?email={0}&code={1}";
    }
}