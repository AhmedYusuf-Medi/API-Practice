using Data.Base.Contracts;

#nullable disable

namespace Data.Models
{
    public class Meme : Entity<long>
    {
        public string Title { get; set; }

        public string PostLink { get; set; }

        public string Url { get; set; }

        public string Author { get; set; }

        public int RedditPostLikes { get; set; }

        public bool IsNotSafeForWork { get; set; }

        public bool IsSpoiler { get; set; }

        public ICollection<MemePreviewLink> PreviewLinks { get; set; } = new HashSet<MemePreviewLink>();
    }
}
