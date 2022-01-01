namespace CarShop.Models.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Entity<T> : IModifiable, IEntity<T>
    {
        [Key]
        public T Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}