using Common.Fsm;

namespace RogueDungeon.Enemies.HiveMind
{
    public abstract class HiveMindState : ITypeBasedTransitionableState, IEnterableState, IExitableState, ITickableState
    {
        protected readonly HiveMindContext Context;

        /// <summary>
        /// Denotes if the current frame should be added to "enemies are not bothering player" time
        /// </summary>
        protected abstract bool IsSlackFrame { get; }

        protected HiveMindState(HiveMindContext context) => 
            Context = context;

        public abstract void CheckTransitions(ITypeBasedStateChanger stateChanger);

        public virtual void Tick(float timeDelta)
        {
            if(IsSlackFrame)
                Context.SlackTime += timeDelta;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}