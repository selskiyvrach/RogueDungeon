using RogueDungeon.Actions;

namespace RogueDungeon.Characters
{
    public abstract class CharacterController
    {
        public Character Character { get; }
        public Action CurrentAction { get; private set; }

        protected CharacterController(Character character) => 
            Character = character;

        public virtual void Tick()
        {
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