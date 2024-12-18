namespace RogueDungeon.Parameters
{
    public abstract class ParameterDecorator<T> : IParameter<T> where T : IParameter
    {
        private readonly T _decorated;

        public float Value => GetValue(_decorated);

        public ParameterDecorator(T decorated) => 
            _decorated = decorated;

        protected abstract float GetValue(T decorated);
    }
}