using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using RogueDungeon.Logging;

namespace RogueDungeon.StateMachine
{
    public sealed class FinishableState : State, IFinishableState
    {
        private readonly IFinishable _finishable;

        public bool IsFinished => _finishable.IsFinished;

        public FinishableState(IFinishable finishable) => 
            _finishable = finishable;
    }

    public interface IStateWithHandlers
    {
        void AddStateEnterHandler(IStateEnterHandler handler);
        void AddStateExitHandler(IStateExitHandler handler);
        void AddStateTickHandler(IStateTickHandler handler);
        void AddAllHandlerInterfaces(IStateHandler handler);
        bool RemoveHandler<T>(T handler) where T : IStateHandler;
    }

    public class State : IState, IEnterable, IExitable, ITickable, IStateWithHandlers, IDebugName
    {
        private Lazy<HashSet<IStateEnterHandler>> _stateEnterHandlers;
        private Lazy<HashSet<IStateTickHandler>> _stateTickHandlers;
        private Lazy<HashSet<IStateExitHandler>> _stateExitHandlers;

        public string DebugName { get; set; }

        public void AddStateEnterHandler(IStateEnterHandler handler) => 
            _stateEnterHandlers.Value.Add(handler);

        public void AddStateExitHandler(IStateExitHandler handler) => 
            _stateExitHandlers.Value.Add(handler);

        public void AddStateTickHandler(IStateTickHandler handler) => 
            _stateTickHandlers.Value.Add(handler);

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
                IStateEnterHandler enterHandler => _stateEnterHandlers.WeakValue?.Remove(enterHandler) ?? false,
                IStateTickHandler tickHandler => _stateTickHandlers.WeakValue?.Remove(tickHandler) ?? false,
                IStateExitHandler exitHandler => _stateExitHandlers.WeakValue?.Remove(exitHandler) ?? false,
                _ => throw new InvalidEnumArgumentException()
            };

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