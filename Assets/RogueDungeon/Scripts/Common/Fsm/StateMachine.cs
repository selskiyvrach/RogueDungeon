using System;
using System.Collections.Generic;
using Common.UtilsDotNet;

namespace Common.Fsm
{
    public class StateMachine
    {
        private readonly IStateTransitionStrategy _transitionStrategy;
        private readonly ILogger _logger;
        private readonly HashSet<IState> _transitionsHistory = new();
        private IState _currentState;

        public StateMachine(IStateTransitionStrategy transitionStrategy, ILogger logger = null)
        {
            _transitionStrategy = transitionStrategy;
            _logger = logger ?? new DefaultLogger();
        }

        public void Enable() => 
            ChangeState(_transitionStrategy.GetStartState()); 

        public void Tick(float timeDelta)
        {
            (_currentState as ITickableState)?.Tick(timeDelta);
            _transitionsHistory.Clear();
            TryTransition();
        }

        public void ChangeState(IState newState)
        {
            _logger?.Log($"Fsm [{this.TypeName()}]. {_currentState} -> {newState}");
            (_currentState as IExitableState)?.Exit();
            _currentState = newState;
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            (_currentState as IEnterableState)?.Enter();
            TryTransition();
        }

        private void TryTransition()
        {
            if(_transitionStrategy.GetTransition(_currentState) is {} state)
                ChangeState(state);
        }
    }
}