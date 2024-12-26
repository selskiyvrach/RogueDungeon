namespace Common.Parameters
{
    public class ModifiedParameter<T> : ParameterDecorator<T> where T : IParameterDefinition
    {
        private readonly IParameterModifier<T> _parameterModifier;

        public ModifiedParameter(IParameter<T> decorated, IParameterModifier<T> parameterModifier) : base(decorated) => 
            _parameterModifier = parameterModifier;

        protected override float GetValue(IParameter<T> decorated) => 
            _parameterModifier.GetModifiedValue(decorated);
    }
}