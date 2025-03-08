using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public abstract class HiveMindState : ITypeBasedTransitionableState, IEnterableState, IExitableState, ITickableState
    {
        public abstract void CheckTransitions(ITypeBasedStateChanger stateChanger);

        public virtual void Tick(float timeDelta)
        {
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}