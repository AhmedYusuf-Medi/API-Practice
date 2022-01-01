namespace CarShop.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JoinEntity : IModifiable
    {
        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}