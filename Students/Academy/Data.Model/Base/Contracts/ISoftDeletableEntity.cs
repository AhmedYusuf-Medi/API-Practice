namespace Data.Base.Contracts
{
    public interface ISoftDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
