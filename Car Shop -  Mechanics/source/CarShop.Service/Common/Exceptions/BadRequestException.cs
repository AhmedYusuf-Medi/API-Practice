using System;

namespace CarShop.Service.Common.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message)
             : base(message) { }
    }
}