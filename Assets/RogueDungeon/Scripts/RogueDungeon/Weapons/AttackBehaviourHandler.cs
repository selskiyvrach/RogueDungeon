using Zenject;

namespace RogueDungeon.Weapons
{
    public abstract class AttackBehaviourHandler : IInitializable
    {
        protected readonly IAttackBehaviour Behaviour;

        protected AttackBehaviourHandler(IAttackBehaviour attackBehaviour) => 
            Behaviour = attackBehaviour;

        public void Initialize()
        {
            Behaviour.OnIdle += HandleIdle;
            Behaviour.OnHitKeyframe += HandleHit;
            Behaviour.OnPrepareAttackStarted += HandlePrepare;
            Behaviour.OnExecuteAttackStarted += HandleExecute;
            Behaviour.OnFinishAttackStarted += HandleFinish;
        }

        protected abstract void HandleIdle();
        protected abstract void HandleExecute();
        protected abstract void HandleFinish();
        protected abstract void HandlePrepare();
        protected abstract void HandleHit();
    }
}