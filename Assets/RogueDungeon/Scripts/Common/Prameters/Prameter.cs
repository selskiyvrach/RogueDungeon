namespace Common.Prameters
{
    public abstract class Parameter
    {
        public float Value { get; }
        public Type ParamType { get; }

        public enum Type
        {
            Flat,
            Percent
        }

        protected Parameter(float value, Type paramType = Type.Flat)
        {
            Value = value;
            ParamType = paramType;
        }
    }
}