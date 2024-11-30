using System;
using System.Collections.Generic;
using Common.DebugTools;
using Common.Properties;
using UniRx;
using Logger = Common.DebugTools.Logger;

namespace Common.FSM
{
    public class AutoRunner : IDisposable
    {
        private readonly StateMachine _stateMachine;
        private readonly IDisposable _sub;

        public AutoRunner(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _stateMachine.Initialize();
            _sub = Observable.EveryUpdate().Subscribe(_ => _stateMachine.Tick());
        }

        public void Dispose()
        {
            _stateMachine.Stop();
            _sub?.Dispose();
        }
    }

    public class StateMachine : IReadOnlyProperty<IState>, IDebugName
    {
        private readonly StatesContainer _statesContainer;
        private readonly TransitionsContainer _transitionsContainer;
        private readonly HashSet<IState> _transitionsThisFrame = new();
        private IState _currentState;

        public IState Value => _currentState;
        public string DebugName { get; set; }

        public StateMachine(StatesContainer statesContainer, TransitionsContainer transitionsContainer)
        {
            _statesContainer = statesContainer;
            _transitionsContainer = transitionsContainer;
        }

        public void Tick()
        {
            (_currentState as ITickable)?.Tick();
            ProcessTransitions();
        }

        public void Initialize() => 
            SwitchToState(_statesContainer.GetStartState());

        private void ProcessTransitions()
        {
            _transitionsThisFrame.Clear();
            while (true)
            {
                if (!_transitionsContainer.CanTransition(_statesContainer, out var newState))
                    break;
                
                if (!_transitionsThisFrame.Add(newState))
                {
                    Logger.LogError(this, "Infinite transition cycle detected: " + string.Join(" -> ", _transitionsThisFrame) + " " + newState);
                    break;
                }
                SwitchToState(newState);
            }
        }

        public void Stop() => 
            ExitCurrentState();

        private void SwitchToState(IState newState)
        {
            ExitCurrentState();
            _currentState = newState;
            (_currentState as IEnterable)?.Enter();
        }

        private void ExitCurrentState() => 
            (_currentState as IExitable)?.Exit();
    }
}