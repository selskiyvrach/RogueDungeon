using System;

namespace RogueDungeon.StateMachine
{
    public class IsInStateCondition<T> : ICondition where T : IState
    {
        private readonly ICurrentStateProvider _currentStateProvider;
        private readonly Type _targetType;

        public IsInStateCondition(ICurrentStateProvider currentStateProvider)
        {
            _currentStateProvider = currentStateProvider;
            _targetType = typeof(T);
        }

        public bool IsMet() => 
            _currentStateProvider.CurrentState.GetType() == _targetType;
    }
}