using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request.Meme
{
    public class CreateMemeRequestModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string Author { get; set; }


        public bool IsNotSafeForWork { get; set; }

        public bool IsSpoiler { get; set; }
    }
}
