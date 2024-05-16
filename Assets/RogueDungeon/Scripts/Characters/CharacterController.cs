using RogueDungeon.Actions;
using RogueDungeon.Data.Stats;
using RogueDungeon.Health;

namespace RogueDungeon.Characters
{
    public abstract class CharacterController
    {
        public Character Character { get; }
        public Action CurrentAction { get; private set; }

        protected CharacterController(Character character)
        {
            Character = character;
            var hpAmount = Character.GetStat(Constants.HP);
            Character.Health.SetHealth(hpAmount, hpAmount, HealthChangeReason.Recalculated);
            Character.Health.OnChanged += reason => Character.HealthDisplay.HandleHealthChanged(Character.Health, reason);
            Character.HealthDisplay.HandleHealthChanged(Character.Health, HealthChangeReason.Recalculated);
        }

        public virtual void Tick()
        {
            Character.HealthDisplay.Tick();
            if(CurrentAction == null)
                return;
            CurrentAction.Tick();
            Character.Animator.UpdateState((float)CurrentAction.CurrentFrame / CurrentAction.Frames);
            if (CurrentAction.IsFinished)
                StopCurrentAction();
        }

        protected void StartAction(Action action)
        {
            if(CurrentAction != null)
                StopCurrentAction();
            CurrentAction = action;
            CurrentAction.Start(Character);
            Character.Animator.SetState(CurrentAction.AnimationName);
        }

        protected void StopCurrentAction()
        {
            CurrentAction?.Stop();
            CurrentAction = null;
            Character.Animator.SetState(null);
        }

        protected void ResetAnimation() => 
            Character.Animator.UpdateState(0);
    }
}