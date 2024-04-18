using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private CharacterConfig _playerConfig;
        [SerializeField] private CharacterConfig _enemyConfig;
        private Character _player;
        private Character _enemy;

        private void Start()
        {
            _player = CharacterFactory.Create(_playerConfig, transform);
            _enemy = CharacterFactory.Create(_enemyConfig, transform);

            _player.CombatState.Enemy = _enemy;
            _enemy.CombatState.Enemy = _player;
            
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            _player.Tick();
            _enemy.Tick();
        }
    }
}