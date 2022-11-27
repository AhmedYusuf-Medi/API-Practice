using Data.Models;
using System.Text.Json.Serialization;

namespace Api.Models.Response.Seeder
{
    public class MemeApiResponseModel
    {
        [JsonPropertyName("postLink")]
        public string PostLink { get; set; }

        [JsonPropertyName("subreddit")]
        public string Subreddit { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }

        [JsonPropertyName("spoiler")]
        public bool Spoiler { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("ups")]
        public int Ups { get; set; }

        [JsonPropertyName("preview")]
        public List<string> Preview { get; set; }

        public Meme ToEntity() => new Meme
        {
            Title = Title,
            PostLink = PostLink,
            Url = Url,
            Author = Author,
            RedditPostLikes = Ups,
            IsNotSafeForWork = Nsfw,
            IsSpoiler = Spoiler,
            PreviewLinks = Preview?.Select(previewUrl => new MemePreviewLink() { Url = previewUrl }).ToList()
        };
    }
}
