namespace Api.Models.Base
{
    public interface IInfoResult
    {
        public string? Message { get; set; }

        public bool IsSuccess { get; set; }

        public string? ExceptionSource { get; set; }
    }
}
