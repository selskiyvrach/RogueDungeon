using System;
using Common.Providers;

namespace Common.FSM
{
    public class IsInStateOfTypeCondition<T> : ICondition where T : IState
    {
        private readonly IProvider<IState> _currentStateProvider;
        private readonly Type _targetType;

        public IsInStateOfTypeCondition(IProvider<IState> currentStateProvider)
        {
            _currentStateProvider = currentStateProvider;
            _targetType = typeof(T);
        }

        public bool IsMet() => 
            _currentStateProvider.value.GetType() == _targetType;
    }
}