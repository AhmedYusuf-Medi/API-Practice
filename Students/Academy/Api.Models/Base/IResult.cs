namespace Api.Models.Base
{
    public interface IResult<TResult>
    {
        public TResult? Payload { get; set; }
    }
}
