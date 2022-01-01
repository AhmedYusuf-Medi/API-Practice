namespace CarShop.Models.Base
{
    using System;

    public interface IEntity<T>
    {
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}