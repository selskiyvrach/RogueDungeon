using RogueDungeon.Actions;

namespace RogueDungeon.Characters
{
    public abstract class CharacterActionsController
    {
        private readonly Character _character;
        
        public Action CurrentAction { get; private set; }

        protected CharacterActionsController(Character character) => 
            _character = character;

        public virtual void Tick()
        {
            if(CurrentAction == null)
                return;
            CurrentAction.Tick();
            _character.Animator.UpdateState((float)CurrentAction.CurrentFrame / CurrentAction.Frames);
            if (!CurrentAction.IsFinished)
                return;
            CurrentAction.Stop();
            CurrentAction = null;
            _character.Animator.SetState(null);
        }

        protected void StartAction(Action action)
        {
            CurrentAction = action;
            CurrentAction.Start(_character);
            _character.Animator.SetState(CurrentAction.AnimationName);
            _character.Animator.UpdateState(0);
        }
    }
}