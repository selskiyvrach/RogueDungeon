namespace RogueDungeon.Entities.Properties
{
    public interface IProperty<T> : IReadOnlyProperty<T>
    {
        public new T Value { get; set; }
    }
}