using Common.Fsm;
using Common.Lifecycle;

namespace RogueDungeon.Enemies.HiveMind
{
    public abstract class HiveMindState : ITypeBasedTransitionableState, IEnterableState, IExitableState, ITickable
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