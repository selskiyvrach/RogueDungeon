using System;
using System.Collections.Generic;
using Common.Lifecycle;
using Common.UtilsDotNet;

namespace Common.Fsm
{
    public class StateMachine
    {
        private readonly IStateTransitionStrategy _stateTransitionStrategy;
        private readonly ILogger _logger;
        private readonly HashSet<IState> _transitionsHistory = new();
        private readonly string _debugName;
        private readonly int _instanceId;

        private IState _currentState;
        private static int _instanceCount;

        public StateMachine(IStateTransitionStrategy stateTransitionStrategy, string debugName = "", ILogger logger = null)
        {
            _stateTransitionStrategy = stateTransitionStrategy;
            _debugName = debugName;
            _logger = logger ?? new DefaultLogger();
            _instanceId = _instanceCount++;
        }

        public void Initialize() => 
            ChangeState(_stateTransitionStrategy.GetStartState());

        public void Tick(float timeDelta)
        {
            (_currentState as ITickable)?.Tick(timeDelta);
            _transitionsHistory.Clear();
            TryTransition();
        }

        public void ChangeState(IState newState)
        {
            _logger?.Log($"[FsmName: {_debugName} FsmId: {_instanceId}]. {_currentState} -> {newState}");
            (_currentState as IExitableState)?.Exit();
            _currentState = newState;
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            (_currentState as IEnterableState)?.Enter();
            TryTransition();
        }

        private void TryTransition()
        {
            if(_stateTransitionStrategy.GetTransition(_currentState) is {} state)
                ChangeState(state);
        }
    }
}