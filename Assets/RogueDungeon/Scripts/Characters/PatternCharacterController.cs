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
}