namespace RogueDungeon.Entities.Properties
{
    public interface IReadOnlyProperty<out T>
    {
        public T Value { get; }
    }
}