using System.Text.Json.Serialization;

namespace Api.Models.Response.Seeder
{
    public class MemesApiResponseModel
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("memes")]
        public ICollection<MemeApiResponseModel> Memes { get; set; } = new HashSet<MemeApiResponseModel>();
    }
}
