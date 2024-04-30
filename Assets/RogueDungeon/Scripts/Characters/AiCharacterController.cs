using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Utils;

namespace RogueDungeon.Characters
{
    public class AiCharacterController : CharacterController
    {
        private readonly AttackPattern[] _patterns;
        
        private AttackAction[] _currentPatternActions;
        private int _chillFramesLeft;
        private int _currentActionIndex;
        
        public AttackPattern CurrentPattern { get; private set; }

        public AiCharacterController(Character character, AttackPattern[] patterns) : base(character) => 
            _patterns = patterns;

        public override void Tick()
        {
            base.Tick();
            
            if(CurrentPattern == null)
                return;
            
            if (CurrentAction != null)
                return;

            if (_currentActionIndex < _currentPatternActions.Length)
            {
                StartAction(_currentPatternActions[_currentActionIndex++]);
                return;
            }

            if (_chillFramesLeft-- == 0) 
                CurrentPattern = null;
        }

        public void StartNewPattern()
        {
            CurrentPattern = _patterns.Where(n => (n.SuitableForPositions & Character.CombatState.Position) != 0).ToList().Random();
            _currentPatternActions = CurrentPattern.AttackConfigs.Select(n => new AttackAction(n)).ToArray();
            _currentActionIndex = 0;
            _chillFramesLeft = CurrentPattern.ChillFrames;
        }
    }
}