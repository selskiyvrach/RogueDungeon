using System;
using System.Collections.Generic;
using Common.Behaviours;
using Common.DotNetUtils;

namespace Common.Fsm
{
    public abstract class StateMachine : Behaviour, IStateChanger
    {
        private readonly IStatesFactory _statesFactory;
        private readonly HashSet<IState> _transitionsHistory = new();
        private IState _currentState;

        protected StateMachine(IStatesFactory statesFactory) => 
            _statesFactory = statesFactory;

        public override void Enable()
        {
            base.Enable();
            ToStartState();
        }

        protected abstract void ToStartState();

        public override void Tick(float timeDelta)
        {
            (_currentState as ITickableState)?.Tick(timeDelta);
            _currentState.CheckTransitions(this);
        }

        public void To<T>() where T : IState
        {
            _transitionsHistory.Clear();
            (_currentState as IExitableState)?.Exit();
            _currentState = _statesFactory.Create<T>();
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            _currentState.Enter();
            _currentState.CheckTransitions(this);
        }
    }
}