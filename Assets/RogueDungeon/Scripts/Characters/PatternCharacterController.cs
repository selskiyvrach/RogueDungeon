namespace RogueDungeon.Characters
{
    public class PatternCharacterController
    {
        private readonly Character _character;
        private readonly int _waitFrames;
        private readonly string[] _moves;
        
        private int _waitFramesLeft;
        private int _movesLeft;

        public PatternCharacterController(Character character, int waitFrames, string[] moves)
        {
            _character = character;
            _waitFrames = waitFrames;
            _moves = moves;
        }

        public void Tick()
        {
            _character.Tick();
            
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
            _character.OnCommand(_moves[^_movesLeft--]);
        }
    }
}