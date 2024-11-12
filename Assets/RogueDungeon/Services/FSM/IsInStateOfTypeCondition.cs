using System;

namespace RogueDungeon.Services.FSM
{
    public class IsInStateOfTypeCondition<T> : ICondition where T : IState
    {
        private readonly ICurrentStateProvider _currentStateProvider;
        private readonly Type _targetType;

        public IsInStateOfTypeCondition(ICurrentStateProvider currentStateProvider)
        {
            _currentStateProvider = currentStateProvider;
            _targetType = typeof(T);
        }

        public bool IsMet() => 
            _currentStateProvider.CurrentState.GetType() == _targetType;
    }
}