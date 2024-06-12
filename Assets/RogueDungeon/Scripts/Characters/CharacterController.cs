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
            Character.Health.OnChanged += reason => Character.HealthDisplay.HandleChanged(Character.Health, reason);
            Character.HealthDisplay.HandleChanged(Character.Health, ResourceChangeReason.Recalculated);
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

            if (!CurrentAction.IsFinished) 
                return;
            
            if (CurrentAction == _staggerAction) 
                Character.CombatState.IsStaggered = false;
            StopCurrentAction();
        }

        private void HandleBalance()
        {
            if(Character.Health.IsDepleted)
                return;
            
            if (!Character.Balance.IsDepleted) 
                return;
            
            StopCurrentAction();
            StartAction(_staggerAction);
            Character.CombatState.IsStaggered = true;
            Character.Balance.Restore(Character.Balance.Max);
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
    }
}