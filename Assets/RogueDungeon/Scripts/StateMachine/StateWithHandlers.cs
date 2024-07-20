using System;
using System.Collections.Generic;

namespace RogueDungeon.StateMachine
{
    public class StateWithHandlers : IState, IEnterable, IExitable, ITickable
    {
        private List<IStateEnterHandler> _stateEnterHandlers;
        private List<IStateTickHandler> _stateTickHandlers;
        private List<IStateExitHandler> _stateExitHandlers;

        /// <summary>
        /// Adds handles as a specific interface derived from IStateHandler
        /// </summary>
        public void AddHandlerInterface<T>(T handler) where T : IStateHandler
        {
            var type = typeof(T);

            if (type == typeof(IStateEnterHandler))
                (_stateEnterHandlers ??= new List<IStateEnterHandler>()).Add((IStateEnterHandler)handler);
            else if(type == typeof(IStateTickHandler))
                (_stateTickHandlers ??= new List<IStateTickHandler>()).Add((IStateTickHandler)handler);
            else if(type == typeof(IStateExitHandler)) 
                (_stateExitHandlers ??= new List<IStateExitHandler>()).Add((IStateExitHandler)handler);
            else
                throw new Exception("Handler type has not been specified");
        }
        
        /// <summary>
        /// Adds handler as all of its handler interfaces
        /// </summary>
        public void AddAllHandlerInterfaces(IStateHandler handler)
        {
            if(handler is IStateEnterHandler enterHandler)
                AddHandlerInterface(enterHandler);
            if(handler is IStateTickHandler tickHandler)
                AddHandlerInterface(tickHandler);
            if(handler is IStateExitHandler exitHandler) 
                AddHandlerInterface(exitHandler);
        }
        
        public bool RemoveHandler<T>(T handler) where T : IStateHandler =>
            handler switch
            {
                IStateEnterHandler enterHandler => _stateEnterHandlers?.Remove(enterHandler) ?? false,
                IStateTickHandler tickHandler => _stateTickHandlers?.Remove(tickHandler) ?? false,
                IStateExitHandler exitHandler => _stateExitHandlers?.Remove(exitHandler) ?? false,
                _ => false
            };

        public void Enter()
        {
            if(_stateEnterHandlers == null || _stateEnterHandlers.Count == 0)
                return;
            
            foreach (var handler in _stateEnterHandlers) 
                handler.OnEnter();
        }

        public void Exit()
        {
            if(_stateExitHandlers == null || _stateExitHandlers.Count == 0)
                return;
            
            foreach (var handler in _stateExitHandlers) 
                handler.OnExit();
        }

        public void Tick()
        {
            if(_stateTickHandlers == null || _stateTickHandlers.Count == 0)
                return;
            
            foreach (var handler in _stateTickHandlers) 
                handler.OnTick();
        }
    }
}