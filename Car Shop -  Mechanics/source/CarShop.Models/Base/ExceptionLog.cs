namespace CarShop.Models.Base
{
    using System;

    public class ExceptionLog : DeletableEntity<Guid>
    {
        public string ExceptionMessage { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
        public bool IsChecked { get; set; }
    }
}