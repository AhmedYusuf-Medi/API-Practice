namespace Data.Base.Contracts
{
    public  class SoftDeletableEntity<TKey> : Entity<TKey>, ISoftDeletableEntity
        where TKey : IEquatable<TKey>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
