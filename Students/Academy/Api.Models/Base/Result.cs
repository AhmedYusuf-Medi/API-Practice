namespace Api.Models.Base

#nullable enable
{
    public class Result<TResult> : InfoResult, IResult<TResult>
    {
        public TResult? Payload { get; set; }
    }
}
