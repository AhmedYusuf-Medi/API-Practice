namespace Data.Base.Contracts
{
    public interface IAuditableEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
