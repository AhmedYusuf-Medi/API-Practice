namespace Data.Base.Contracts
{
    public interface ICompositeEntity<TKeyA, TKeyB>
        where TKeyA : IEquatable<TKeyA>
        where TKeyB : IEquatable<TKeyB>
    {
        public abstract TKeyA KeyA { get; set; }

        public abstract TKeyB KeyB { get; set; }
    }
}
