using System.Collections.Generic;
using Common.DebugTools;
using Common.DotNetUtils;

namespace Common.FSM
{
    public sealed class State : IState, IEnterable, IExitable, ITickable, IDebugName
    {
        private readonly HashSet<IStateEnterHandler> _stateEnterHandlers = new();
        private readonly HashSet<IStateTickHandler> _stateTickHandlers = new();
        private readonly HashSet<IStateExitHandler> _stateExitHandlers = new();

        public string DebugName { get; set; }

        /// <summary>
        /// Adds handler as all of its handler interfaces
        /// </summary>
        public void AddHandler(IStateHandler handler)
        {
            if(handler is IStateEnterHandler enterHandler)
                _stateEnterHandlers.Add(enterHandler);
            if(handler is IStateTickHandler tickHandler)
                _stateTickHandlers.Add(tickHandler);
            if(handler is IStateExitHandler exitHandler) 
                _stateExitHandlers.Add(exitHandler);
        }

        public void Enter()
        {
            foreach (var handler in _stateEnterHandlers.SafeEnumerable()) 
                handler.OnEnter();
        }

        public void Exit()
        {
            foreach (var handler in _stateExitHandlers.SafeEnumerable()) 
                handler.OnExit();
        }

        public void Tick()
        {
            foreach (var handler in _stateTickHandlers.SafeEnumerable()) 
                handler.OnTick();
        }
    }
}