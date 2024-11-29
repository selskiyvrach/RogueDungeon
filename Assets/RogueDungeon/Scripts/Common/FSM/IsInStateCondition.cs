using Common.Providers;

namespace Common.FSM
{
    public class IsInStateCondition : ICondition
    {
        private readonly IProvider<IState> _currentStateProvider;
        private readonly IState _targetState;

        public IsInStateCondition(IProvider<IState> currentStateProvider, IState targetState)
        {
            _currentStateProvider = currentStateProvider;
            _targetState = targetState;
        }

        public bool IsMet() => 
            _currentStateProvider.value == _targetState;
    }
    
    public class ValueCondition : ICondition
    {
        private readonly IProvider<bool> _provider;

        public ValueCondition(IProvider<bool> provider) => 
            _provider = provider;

        public bool IsMet() => 
            _provider.value;
    }
}