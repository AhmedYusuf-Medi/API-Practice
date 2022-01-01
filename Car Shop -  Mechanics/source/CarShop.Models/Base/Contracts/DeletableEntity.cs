namespace CarShop.Models.Base
{
    using System;

    public class DeletableEntity<T> : Entity<T>, IDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}