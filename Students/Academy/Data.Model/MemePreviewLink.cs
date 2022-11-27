using Data.Base.Contracts;

#nullable disable

namespace Data.Models
{
    public class MemePreviewLink : Entity<long>
    {
        public string Url { get; set; }

        public long MemeId { get; set; }

        public Meme Meme { get; set; }
    }
}
