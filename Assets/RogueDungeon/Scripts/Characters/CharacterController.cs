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
            if (!CurrentAction.IsFinished)
                return;
            CurrentAction.Stop();
            CurrentAction = null;
            Character.Animator.SetState(null);
        }

        protected void StartAction(Action action)
        {
            CurrentAction = action;
            CurrentAction.Start(Character);
            Character.Animator.SetState(CurrentAction.AnimationName);
            Character.Animator.UpdateState(0);
        }
    }
}