#nullable enable

namespace Api.Models.Response.MemeApi
{
    public class MemeResponseModel
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? PostLink { get; set; }

        public string? Url { get; set; }

        public string? Author { get; set; }

        public int RedditPostLikes { get; set; }

        public bool IsNotSafeForWork { get; set; }

        public bool IsSpoiler { get; set; }

        public ICollection<MemePreviewLinkResponseModel> PreviewLinks { get; set; } = new List<MemePreviewLinkResponseModel>();
    }
}
