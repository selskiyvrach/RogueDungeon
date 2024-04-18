namespace RogueDungeon.Characters
{
    public abstract class CharacterController
    {
        private readonly Character _character;

        protected CharacterController(Character character) => 
            _character = character;

        public virtual void Tick()
        {
            if(_character.CurrentAction == null)
                return;
            _character.CurrentAction.Tick();
            _character.Animator.UpdateState((float)_character.CurrentAction.CurrentFrame / _character.CurrentAction.Frames);
            if (!_character.CurrentAction.IsFinished)
                return;
            // Debug.Log(Actions.FirstOrDefault(n => n.Value == CurrentAction).Key + " finished");
            _character.CurrentAction.Stop();
            _character.CurrentAction = null;
            _character.Animator.SetState(null);
        }

        /// <summary>
        /// Returns false if command could not execute at the moment 
        /// </summary>
        protected bool OnCommand(string command)
        {
            if (_character.CurrentAction?.IsFinished ?? false)
                _character.CurrentAction = null;

            if (_character.CurrentAction != null)
            {
                // TODO: find out if this behaviour is ok or should it also be a boolean 
                // for now it returns false no matter the action's ability to handle the command
                // so the command will become a coyote time command in any case
                _character.CurrentAction.OnCommand(command);
                return false;
            }

            if (!_character.Actions.TryGetValue(command, out var action))
                return true;

            _character.CurrentAction = action;
            _character.CurrentAction.Start();
            _character.Animator.SetState(_character.CurrentAction.AnimationName);
            _character.Animator.UpdateState(0);
            return true;
            // Debug.Log(command + " started");
        }
    }
}