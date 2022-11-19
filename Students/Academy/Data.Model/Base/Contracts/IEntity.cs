namespace Data.Base.Contracts
{
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
