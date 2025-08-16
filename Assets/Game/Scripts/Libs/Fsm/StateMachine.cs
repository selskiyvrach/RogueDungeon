using System;
using System.Collections.Generic;
using Libs.Lifecycle;
using Libs.Utils.DotNet;

namespace Libs.Fsm
{
    public class StateMachine
    {
        private readonly IStateTransitionStrategy _stateTransitionStrategy;
        private readonly ILogger _logger;
        private readonly HashSet<IState> _transitionsHistory = new();
        private readonly string _debugName;
        private readonly int _instanceId;

        private static int _instanceCount;
        public IState CurrentState { get; private set; }
        public event Action<IState> OnStateChanged;

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
            (CurrentState as ITickable)?.Tick(timeDelta);
            _transitionsHistory.Clear();
            TryTransition();
        }

        public void ChangeState(IState newState)
        {
            _logger?.Log($"[FsmName: {_debugName} FsmId: {_instanceId}]. {CurrentState} -> {newState}");
            if (!_transitionsHistory.Add(newState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());
            
            (CurrentState as IExitableState)?.Exit();
            CurrentState = newState;
            (CurrentState as IEnterableState)?.Enter();
            OnStateChanged?.Invoke(CurrentState);
            TryTransition();
        }

        private void TryTransition()
        {
            if(_stateTransitionStrategy.GetTransition(CurrentState) is {} state)
                ChangeState(state);
        }
    }
}