using System.ComponentModel.DataAnnotations;

namespace Data.Base.Contracts
{
    #nullable disable

    public abstract class Entity<TKey> : IEntity<TKey>, IAuditableEntity
        where TKey : IEquatable<TKey>
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
