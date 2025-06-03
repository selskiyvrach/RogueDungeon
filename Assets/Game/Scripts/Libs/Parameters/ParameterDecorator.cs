namespace Libs.Parameters
{
    public abstract class ParameterDecorator<T> : IParameter<T> where T : IParameterDefinition
    {
        private readonly IParameter<T> _decorated;

        public float Value => GetValue(_decorated);

        public ParameterDecorator(IParameter<T> decorated) => 
            _decorated = decorated;

        protected abstract float GetValue(IParameter<T> decorated);
    }
}