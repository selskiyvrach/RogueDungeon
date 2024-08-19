using System.Collections.Generic;
using RogueDungeon.Logging;
using UnityEngine.Assertions;

namespace RogueDungeon.StateMachine
{
    public class StateMachine : ICurrentStateProvider, IDebugName
    {
        private readonly StatesContainer _statesContainer;
        private readonly TransitionsContainer _transitionsContainer;
        private readonly HashSet<IState> _transitionsThisFrame = new();
        private IState _currentState;
        private bool _isRunning;

        public IState CurrentState => _currentState;

        public string DebugName { get; set; }

        public StateMachine(StatesContainer statesContainer, TransitionsContainer transitionsContainer)
        {
            _statesContainer = statesContainer;
            _transitionsContainer = transitionsContainer;
        }

        public void Run()
        {
            Assert.IsFalse(_isRunning);
            SwitchToState(_statesContainer.GetStartState());
            _isRunning = true;
            ProcessTransitions();
        }

        public void Tick()
        {
            Assert.IsTrue(_isRunning);
            (_currentState as ITickable)?.Tick();
            
            ProcessTransitions();
        }

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

        public void Stop()
        {
            Assert.IsTrue(_isRunning);
            ExitCurrentState();
            _isRunning = false;
        }

        private void SwitchToState(IState newState)
        {
            ExitCurrentState();
            _currentState = newState;
            Logger.Log(this, "Switched to state {0}", _currentState);
            (_currentState as IEnterable)?.Enter();
        }

        private void ExitCurrentState() => 
            (_currentState as IExitable)?.Exit();
    }
}