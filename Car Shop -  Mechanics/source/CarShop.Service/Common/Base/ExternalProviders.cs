namespace CarShop.Service.Common.Base
{
    public static class ExternalProviders
    {
        //Sender
        public const string Sender_Name = "Car shop";
        //Mail inf
        public const string Abv_Account = "carshop-mechanics@abv.bg";
        private const string Abv_Password = "passwordQ1!";
        //Cloudinary settings
        public const string Cloudinary_Name = "diihcd5cx";
        public const string Cloudinary_Key = "653456971911318";
        public const string Cloudinary_Secret = "0F-OawIxly_YApVbCfbyk9-IUgE";
        //Sendgrid
        private const string SendGrid_Password = "passwordpasswordQ1!";
        public const string SendGrid_Secret = "";
        public const string SendGrid_Key = "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";
        //Sendgrid arguments
        public const string SendGrid_ComfirmMail = "Car shop comfirm email verification flow.";
        public const string SendGrid_LinkForVerification = "http://localhost:1920/api/Accounts/verification?email={0}&code={1}";
    }
}