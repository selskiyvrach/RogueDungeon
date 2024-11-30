
using RogueDungeon.Entities.Properties;

namespace Common.FSM
{
    public class IsInStateCondition : ICondition
    {
        private readonly IReadOnlyProperty<IState> _currentStateProvider;
        private readonly IState _targetState;

        public IsInStateCondition(IReadOnlyProperty<IState> currentStateProvider, IState targetState)
        {
            _currentStateProvider = currentStateProvider;
            _targetState = targetState;
        }

        public bool IsMet() => 
            _currentStateProvider.Value == _targetState;
    }
    
    public class ValueCondition : ICondition
    {
        private readonly IReadOnlyProperty<bool> _provider;

        public ValueCondition(IReadOnlyProperty<bool> provider) => 
            _provider = provider;

        public bool IsMet() => 
            _provider.Value;
    }
}