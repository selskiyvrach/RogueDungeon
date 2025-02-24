using System;

namespace Common.Fsm
{
    public class TypeBasedTransitionStrategy : IStateTransitionStrategy, ITypeBasedStateChanger
    {
        private readonly ITypeBasedStatesProvider _statesProvider;
        private Func<ITypeBasedTransitionableState> _startStateGetter;
        private IState _transition;

        public TypeBasedTransitionStrategy(ITypeBasedStatesProvider statesProvider) => 
            _statesProvider = statesProvider;

        public TypeBasedTransitionStrategy SetStartState<T>() where T : class, ITypeBasedTransitionableState
        {
            _startStateGetter = () => _statesProvider.Get<T>();
            return this;
        }

        public IState GetStartState() => 
            _startStateGetter.Invoke();

        public IState GetTransition(IState currentState)
        {
            _transition = null;
            ((ITypeBasedTransitionableState)currentState).CheckTransitions(this);
            return _transition;
        }

        void ITypeBasedStateChanger.ChangeState<T>() => 
            _transition = _statesProvider.Get<T>();
    }
}