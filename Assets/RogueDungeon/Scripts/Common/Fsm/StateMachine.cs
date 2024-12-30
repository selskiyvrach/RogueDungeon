using System;
using System.Collections.Generic;
using Common.UtilsDotNet;

namespace Common.Fsm
{
    public abstract class StateMachine : IStateChanger
    {
        private readonly IStatesFactory _statesFactory;
        private readonly ILogger _logger;
        private readonly HashSet<IState> _transitionsHistory = new();
        private IState _currentState;

        protected StateMachine(IStatesFactory statesFactory, ILogger logger = null)
        {
            _statesFactory = statesFactory;
            _logger = logger ?? new DefaultLogger();
        }

        public virtual void Enable() => 
            ToStartState();

        protected abstract void ToStartState();

        public void Tick(float timeDelta)
        {
            (_currentState as ITickableState)?.Tick(timeDelta);
            _currentState.CheckTransitions(this);
        }

        public void To<T>() where T : class, IState
        {
            _logger?.Log($"Fsm [{this.TypeName()}]. {_currentState?.TypeName()} -> {typeof(T).Name}");
            _transitionsHistory.Clear();
            (_currentState as IExitableState)?.Exit();
            _currentState = _statesFactory.Create<T>();
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            (_currentState as IEnterableState)?.Enter();
            _currentState.CheckTransitions(this);
        }
    }
}