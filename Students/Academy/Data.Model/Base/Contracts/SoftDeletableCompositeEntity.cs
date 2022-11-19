namespace Data.Base.Contracts
{
    public abstract class SoftDeletableCompositeEntity<TKeyA, TKeyB> : CompositeEntity<TKeyA, TKeyB>, ISoftDeletableEntity
        where TKeyA : IEquatable<TKeyA>
        where TKeyB : IEquatable<TKeyB>
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
