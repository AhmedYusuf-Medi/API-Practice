namespace Data.Base.Contracts
{
#nullable disable

    public abstract class CompositeEntity<TKeyA, TKeyB> : ICompositeEntity<TKeyA, TKeyB>, IAuditableEntity, ISoftDeletableEntity
        where TKeyA : IEquatable<TKeyA>
        where TKeyB : IEquatable<TKeyB>
    {
        public TKeyA KeyA { get; set; }

        public TKeyB KeyB { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
