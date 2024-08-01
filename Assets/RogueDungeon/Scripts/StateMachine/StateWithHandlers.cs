using System;
using System.Collections.Generic;

namespace RogueDungeon.StateMachine
{
    public class StateWithHandlers : IState, IEnterable, IExitable, ITickable
    {
        private List<IStateEnterHandler> _stateEnterHandlers;
        private List<IStateTickHandler> _stateTickHandlers;
        private List<IStateExitHandler> _stateExitHandlers;

        public void AddStateEnterHandler(IStateEnterHandler handler) => 
            (_stateEnterHandlers ??= new List<IStateEnterHandler>()).Add(handler);
        
        public void AddStateExitHandler(IStateExitHandler handler) => 
            (_stateExitHandlers ??= new List<IStateExitHandler>()).Add(handler);
        
        public void AddStateTickHandler(IStateTickHandler handler) => 
            (_stateTickHandlers ??= new List<IStateTickHandler>()).Add(handler);
        
        /// <summary>
        /// Adds handler as all of its handler interfaces
        /// </summary>
        public void AddAllHandlerInterfaces(IStateHandler handler)
        {
            if(handler is IStateEnterHandler enterHandler)
                AddStateEnterHandler(enterHandler);
            if(handler is IStateTickHandler tickHandler)
                AddStateTickHandler(tickHandler);
            if(handler is IStateExitHandler exitHandler) 
                AddStateExitHandler(exitHandler);
        }
        
        public bool RemoveHandler<T>(T handler) where T : IStateHandler =>
            handler switch
            {
                IStateEnterHandler enterHandler => _stateEnterHandlers?.Remove(enterHandler) ?? false,
                IStateTickHandler tickHandler => _stateTickHandlers?.Remove(tickHandler) ?? false,
                IStateExitHandler exitHandler => _stateExitHandlers?.Remove(exitHandler) ?? false,
                _ => false
            };

        public virtual void Enter()
        {
            if(_stateEnterHandlers == null || _stateEnterHandlers.Count == 0)
                return;
            
            foreach (var handler in _stateEnterHandlers) 
                handler.OnEnter();
        }

        public virtual void Exit()
        {
            if(_stateExitHandlers == null || _stateExitHandlers.Count == 0)
                return;
            
            foreach (var handler in _stateExitHandlers) 
                handler.OnExit();
        }

        public virtual void Tick()
        {
            if(_stateTickHandlers == null || _stateTickHandlers.Count == 0)
                return;
            
            foreach (var handler in _stateTickHandlers) 
                handler.OnTick();
        }
    }
}