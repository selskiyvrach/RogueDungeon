using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public abstract class HiveMindState : ITypeBasedTransitionableState, IEnterableState, IExitableState
    {
        public abstract void CheckTransitions(ITypeBasedStateChanger stateChanger);
        
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}