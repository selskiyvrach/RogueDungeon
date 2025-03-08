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

        private static int _instanceCount;
        private readonly int _instanceId;

        public IState CurrentState { get; private set; }
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
            (CurrentState as ITickableState)?.Tick(timeDelta);
            _transitionsHistory.Clear();
            TryTransition();
        }

        public void ChangeState(IState newState)
        {
            _logger?.Log($"[FsmName: {DebugName} FsmId: {_instanceId}]. {CurrentState} -> {newState}");
            (CurrentState as IExitableState)?.Exit();
            CurrentState = newState;
            if (!_transitionsHistory.Add(CurrentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            (CurrentState as IEnterableState)?.Enter();
            TryTransition();
        }

        private void TryTransition()
        {
            if(TransitionStrategy.GetTransition(CurrentState) is {} state)
                ChangeState(state);
        }
    }
}