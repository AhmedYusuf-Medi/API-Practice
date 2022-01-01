namespace CarShop.Models.Response
{
    using Newtonsoft.Json;

    public class InfoResponse
    {
        [JsonProperty(nameof(Message))]
        public string Message { get; set; }

        [JsonProperty(nameof(IsSuccess))]
        public bool IsSuccess { get; set; }
    }
}