namespace Common.Fsm
{
    public abstract class State : IState, IExitableState, ITickableState
    {
        public  abstract void Enter();
        public virtual void Exit() { }
        public virtual void Tick(float timeDelta) { }
        public abstract void CheckTransitions(IStateChanger stateChanger);
    }
}