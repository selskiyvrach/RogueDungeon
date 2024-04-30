using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Utils;

namespace RogueDungeon.Characters
{
    public class PatternCharacterActionsController : CharacterActionsController
    {
        private readonly AttackPattern[] _patterns;

        private AttackPattern _currentPattern;
        private AttackAction[] _currentPatternActions;
        
        private int _chillFramesLeft;
        private int _currentActionIndex;

        public PatternCharacterActionsController(Character character, AttackPattern[] patterns) : base(character) => 
            _patterns = patterns;

        public override void Tick()
        {
            base.Tick();
            
            if (CurrentAction != null)
                return;
            
            if (_currentPattern == null) 
                StartNewPattern();

            if (_currentActionIndex < _currentPatternActions.Length)
            {
                StartAction(_currentPatternActions[_currentActionIndex++]);
                return;
            }

            if (_chillFramesLeft-- == 0) 
                _currentPattern = null;
        }

        private void StartNewPattern()
        {
            _currentPattern = _patterns.Random();
            _currentPatternActions = _currentPattern.AttackConfigs.Select(n => new AttackAction(n)).ToArray();
            _currentActionIndex = 0;
            _chillFramesLeft = _currentPattern.ChillFrames;
        }
    }
}