using RogueDungeon.Actions;
using RogueDungeon.CharacterResource;

namespace RogueDungeon.Characters
{
    public abstract class CharacterController
    {
        private readonly StaggerAction _staggerAction;
        public Character Character { get; }
        public Action CurrentAction { get; private set; }

        protected CharacterController(Character character)
        {
            Character = character;
            Character.Health.OnChanged += reason => Character.HealthDisplay.HandleHealthChanged(Character.Health, reason);
            Character.HealthDisplay.HandleHealthChanged(Character.Health, ResourceChangeReason.Recalculated);
            _staggerAction = new StaggerAction(character.Config.StaggerActionConfig);
        }

        public virtual void Tick()
        {
            Character.HealthDisplay.Tick();
            HandleBalance();

            if(CurrentAction == null)
                return;
            CurrentAction.Tick();
            Character.Animator.UpdateState((float)CurrentAction.CurrentFrame / CurrentAction.Frames);
            if (CurrentAction.IsFinished)
                StopCurrentAction();
        }

        private void HandleBalance()
        {
            if(Character.Health.IsDepleted)
                return;
            
            var balance = Character.Balance;
            // no check for current stagger since the stagger should restart on the next balance depletion
            // should not be empty if not hit again since restoration happens later in this method
            if (balance.IsDepleted)
            {
                StopCurrentAction();
                StartAction(_staggerAction);
                Character.CombatState.IsStaggered = true;
            }
            
            if(CurrentAction == _staggerAction && CurrentAction.IsFinished)
                Character.CombatState.IsStaggered = false;

            if (!balance.IsFull)
                balance.Restore(60f / Character.GetStat("BalanceRestorationFrames"));
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