namespace CarShop.Models.Base
{
    using System;

    public class DeletableJoinEntity : JoinEntity, IDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}