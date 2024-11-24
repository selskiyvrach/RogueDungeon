namespace RogueDungeon.Entities.Properties
{
    public abstract class Property
    {
    }

    public class Property<T> : Property
    {
        public T Value { get; set; }

        public Property()
        {
        }
        
        public Property(T value) => 
            Value = value;
    }
}