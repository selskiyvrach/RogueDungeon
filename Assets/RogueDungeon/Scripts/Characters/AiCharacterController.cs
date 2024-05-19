using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Utils;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public class AiCharacterController : CharacterController
    {
        private readonly List<AttackPattern> _patterns;
        
        private AttackAction[] _currentPatternActions;
        private int _currentActionIndex;
        private bool _handlingDeath;
        
        public AttackPattern CurrentPattern { get; private set; }

        public AiCharacterController(Character character, IEnumerable<AttackPattern> patterns) : base(character) => 
            _patterns = new List<AttackPattern>(patterns);

        public override void Tick()
        {
            base.Tick();

            if (Character.Health.IsDepleted)
            {
                if (_handlingDeath) 
                    return;
                
                StopCurrentAction();
                CurrentPattern = null;
                StartAction(new DeathAction(((EnemyCharacterConfig)Character.Config).DeathActionConfig));
                _handlingDeath = true;
                return;
            }

            if(CurrentPattern == null)
                return;

            if (CurrentAction != null)
                return;

            if (_currentActionIndex < _currentPatternActions.Length)
            {
                StartAction(_currentPatternActions[_currentActionIndex++]);
                return;
            }

            CurrentPattern = null;
        }

        public void StartNewPattern()
        {
            if (Character.Health.IsDepleted)
            {
                Debug.LogError("Cannot start a pattern - the character is dead");
                return;
            }

            var patterns = _patterns.Where(n => (n.suitableForPosition & Character.CombatState.Position) != 0).ToList();
            if (patterns.Count == 0)
            {
                Debug.LogError("Cannot start a pattern - no suitable patterns for the position");
                return;
            }

            CurrentPattern = patterns.Random();
            _currentPatternActions = CurrentPattern.Attacks.Select(n => new AttackAction(((EnemyCharacterConfig)Character.Config).GetAttackConfig(n))).ToArray();
            _currentActionIndex = 0;
        }

        public void StopCurrentPattern() => 
            CurrentPattern = null;
    }
}