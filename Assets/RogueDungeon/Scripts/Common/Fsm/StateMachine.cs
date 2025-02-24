using System;
using System.Collections.Generic;
using Common.UtilsDotNet;

namespace Common.Fsm
{
    public class StateMachine
    {
        public readonly IStateTransitionStrategy TransitionStrategy;
        private readonly ILogger _logger;
        private readonly HashSet<IState> _transitionsHistory = new();
        private IState _currentState;

        private static int _instanceCount;
        private readonly int _instanceId;

        public string DebugName { get; set; }

        public StateMachine(IStateTransitionStrategy transitionStrategy, string debugName = "", ILogger logger = null)
        {
            TransitionStrategy = transitionStrategy;
            DebugName = debugName;
            _logger = logger ?? new DefaultLogger();
            _instanceId = _instanceCount++;
        }

        public void Enable() => 
            ChangeState(TransitionStrategy.GetStartState());

        public void Tick(float timeDelta)
        {
            (_currentState as ITickableState)?.Tick(timeDelta);
            _transitionsHistory.Clear();
            TryTransition();
        }

        public void ChangeState(IState newState)
        {
            _logger?.Log($"[Fsm: '{DebugName}' ({_instanceId})]. {_currentState} -> {newState}");
            (_currentState as IExitableState)?.Exit();
            _currentState = newState;
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            (_currentState as IEnterableState)?.Enter();
            TryTransition();
        }

        private void TryTransition()
        {
            if(TransitionStrategy.GetTransition(_currentState) is {} state)
                ChangeState(state);
        }
    }
}