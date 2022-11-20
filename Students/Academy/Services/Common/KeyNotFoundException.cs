namespace Services.Common
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string message)
              : base(message) 
        {
        }
    }
}
