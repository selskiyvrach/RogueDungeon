using RogueDungeon.Actions;
using RogueDungeon.Characters;
using UnityEngine;
using Animator = RogueDungeon.Animations.Animator;

namespace RogueDungeon
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private CharacterConfig _playerConfig;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private CharacterConfig _enemyConfig;
        [SerializeField] private Animator _enemyAnimator;
        private KeyboardCharacterController _player;
        private PatternCharacterController _enemy;

        private void Start()
        {
            var playerCharacter = new Character(_playerConfig, _playerAnimator);
            playerCharacter.Actions.Add("Attack", new AttackAction(playerCharacter, playerCharacter.Config.UnarmedAttack, DodgeState.NotDodging));
            playerCharacter.Actions.Add("Block", new BlockAction(playerCharacter, playerCharacter.Config.UnarmedBlock));
            playerCharacter.Actions.Add("DodgeLeft", new DodgeAction(playerCharacter, DodgeState.DodgingLeft, playerCharacter.Config.GetActionConfig("DodgeLeft")));
            playerCharacter.Actions.Add("DodgeRight", new DodgeAction(playerCharacter, DodgeState.DodgingRight, playerCharacter.Config.GetActionConfig("DodgeRight")));
            _player = new KeyboardCharacterController(playerCharacter);
            
            
            var enemyCharacter = new Character(_enemyConfig, _enemyAnimator);
            enemyCharacter.Actions.Add("AttackCenter", new AttackAction(enemyCharacter, enemyCharacter.Config.AttackCenter, DodgeState.NotDodging));            
            enemyCharacter.Actions.Add("AttackLeft", new AttackAction(enemyCharacter, enemyCharacter.Config.AttackLeft, DodgeState.DodgingRight));
            enemyCharacter.Actions.Add("AttackRight", new AttackAction(enemyCharacter, enemyCharacter.Config.AttackCenter, DodgeState.DodgingLeft));
            _enemy = new PatternCharacterController(enemyCharacter, 120, new []{"AttackLeft", "AttackRight", "AttackCenter"});

            playerCharacter.CombatState.Enemy = enemyCharacter;
            enemyCharacter.CombatState.Enemy = playerCharacter;
            
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            _player.Tick();
            // _enemy.Tick();        
        }
    }
}