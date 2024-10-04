using System;

namespace RogueDungeon.StateMachine
{
    public interface IStateHandler
    {
    }

    public abstract class ActionStateHandler : IStateHandler
    {
        private readonly Action _action;

        protected ActionStateHandler(Action action) => 
            _action = action;

        protected void Invoke() => 
            _action.Invoke();
    }

    public class ActionStateEnterHandler : ActionStateHandler, IStateEnterHandler
    {
        public ActionStateEnterHandler(Action onEnter) : base(onEnter) {} 

        public void OnEnter() => 
            Invoke();
    }
    
    public class ActionStateExitHandler : ActionStateHandler, IStateExitHandler
    {
        public ActionStateExitHandler(Action onEnter) : base(onEnter) {} 

        public void OnExit() => 
            Invoke();
    }
    
    public class ActionStateTickHandler : ActionStateHandler, IStateTickHandler
    {
        public ActionStateTickHandler(Action onEnter) : base(onEnter) {} 

        public void OnTick() => 
            Invoke();
    }
    
}