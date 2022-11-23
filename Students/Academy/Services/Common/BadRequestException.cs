namespace Services.Common
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
              : base(message)
        {
        }
    }
}
