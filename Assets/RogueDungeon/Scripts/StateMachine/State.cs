using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using RogueDungeon.DebugTools;

namespace RogueDungeon.StateMachine
{
    public sealed class State : IState, IEnterable, IExitable, ITickable, IDebugName
    {
        private Lazy<HashSet<IStateEnterHandler>> _stateEnterHandlers;
        private Lazy<HashSet<IStateTickHandler>> _stateTickHandlers;
        private Lazy<HashSet<IStateExitHandler>> _stateExitHandlers;

        public string DebugName { get; set; }

        /// <summary>
        /// Adds handler as all of its handler interfaces
        /// </summary>
        public void AddHandler(IStateHandler handler)
        {
            if(handler is IStateEnterHandler enterHandler)
                _stateEnterHandlers.Value.Add(enterHandler);
            if(handler is IStateTickHandler tickHandler)
                _stateTickHandlers.Value.Add(tickHandler);
            if(handler is IStateExitHandler exitHandler) 
                _stateExitHandlers.Value.Add(exitHandler);
        }

        public void Enter()
        {
            foreach (var handler in _stateEnterHandlers.WeakValue.SafeEnumerable()) 
                handler.OnEnter();
        }

        public void Exit()
        {
            foreach (var handler in _stateExitHandlers.WeakValue.SafeEnumerable()) 
                handler.OnExit();
        }

        public void Tick()
        {
            foreach (var handler in _stateTickHandlers.WeakValue.SafeEnumerable()) 
                handler.OnTick();
        }
    }
}