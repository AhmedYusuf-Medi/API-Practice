namespace CarShop.Models.Base
{
    using System;

    public interface IModifiable
    {
        DateTime? ModifiedOn { get; set; }
    }
}