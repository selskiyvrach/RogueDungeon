using Common.DotNetUtils;

namespace Common.Prameters
{
    public readonly struct Modifier
    {
        public enum Type
        {
            Flat,
            Percent
        }

        public readonly float Value;
        public readonly object Source;
        public readonly Type type;
        
        public Modifier(float value, object source, Type type = Type.Flat)
        {
            Value = value;
            Source = source.ThrowIfNull();
            this.type = type;
        }
    }
}