namespace CarShop.Models.Response
{
    using Newtonsoft.Json;
    public class Response<T> : InfoResponse
    {
        [JsonProperty(nameof(Payload))]
        public T Payload { get; set; }
    }
}