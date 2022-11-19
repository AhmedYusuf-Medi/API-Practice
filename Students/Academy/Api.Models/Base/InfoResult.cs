#nullable enable
namespace Api.Models.Base
{
    public class InfoResult : IInfoResult
    {
        public string? Message { get; set; }

        public bool IsSuccess { get; set; }

        public string? ExceptionSource { get; set; }
    }
}
