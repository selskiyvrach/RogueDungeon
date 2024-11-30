using System;
using RogueDungeon.Entities.Properties;

namespace Common.FSM
{
    public class IsInStateOfTypeCondition<T> : ICondition where T : IState
    {
        private readonly IReadOnlyProperty<IState> _currentStateProvider;
        private readonly Type _targetType;

        public IsInStateOfTypeCondition(IReadOnlyProperty<IState> currentStateProvider)
        {
            _currentStateProvider = currentStateProvider;
            _targetType = typeof(T);
        }

        public bool IsMet() => 
            _currentStateProvider.Value.GetType() == _targetType;
    }
}