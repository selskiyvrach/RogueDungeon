using System;
using UnityEngine.Assertions;

namespace Common.Fsm
{
    
    public class IdBasedAndTypeBasedCombinedTransitionsStrategy : IStateTransitionStrategy
    {
        private readonly TypeBasedTransitionStrategy _typeBasedTransitionStrategy;
        private readonly IdBasedTransitionStrategy _idBasedTransitionStrategy;
        
        private IStateTransitionStrategy _startTransitionStrategy;

        public IdBasedAndTypeBasedCombinedTransitionsStrategy(TypeBasedTransitionStrategy typeBasedTransitionStrategy, IdBasedTransitionStrategy idBasedTransitionStrategy)
        {
            _typeBasedTransitionStrategy = typeBasedTransitionStrategy;
            _idBasedTransitionStrategy = idBasedTransitionStrategy;
        }

        public void SetStartState<T>() where T : class, ITypeBasedTransitionableState
        {
            _typeBasedTransitionStrategy.SetStartState<T>();
            Assert.IsNull(_startTransitionStrategy);
            _startTransitionStrategy = _typeBasedTransitionStrategy;
        }

        public void SetStartState(string id)
        {
            _idBasedTransitionStrategy.StartStateId = id;
            Assert.IsNull(_startTransitionStrategy);
            _startTransitionStrategy = _idBasedTransitionStrategy;
        }

        public IState GetStartState() => 
            _startTransitionStrategy.GetStartState();

        public IState GetTransition(IState currentState)
        {
            // if(currentState is IIdBasedTransitionableState idBasedState)
            //     return idBasedState.GetTransitionStateId() is { Length: > 0} id 
            //         ? _idBasedTransitionStrategy.Get(id) 
            //         : null;
            // if (currentState is ITypeBasedTransitionableState typeBasedState)
            // {
            //     _typeBasedTransition = null;
            //     typeBasedState.CheckTransitions(this);
            //     return _typeBasedTransition;
            // }
            throw new InvalidCastException();
        }
    }    
    
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