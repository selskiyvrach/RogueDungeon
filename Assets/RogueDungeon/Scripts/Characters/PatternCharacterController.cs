namespace RogueDungeon.Characters
{
    public class PatternCharacterController : CharacterController
    {
        private readonly Character _character;
        private readonly int _waitFrames;
        private readonly string[] _moves;
        
        private int _waitFramesLeft;
        private int _movesLeft;

        public PatternCharacterController(Character character, int waitFrames, string[] moves) : base(character)
        {
            _character = character;
            _waitFrames = waitFrames;
            _moves = moves;
        }

        public override void Tick()
        {
            base.Tick();
            
            if(_character.CurrentAction != null)
                return;
            if(--_waitFramesLeft > 0)
                return;
            if (_movesLeft == 0)
            {
                _waitFramesLeft = _waitFrames;
                _movesLeft = _moves.Length;
                return;
            }
            OnCommand(_moves[^_movesLeft--]);
        }
    }

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