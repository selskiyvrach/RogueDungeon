using RogueDungeon.Characters;
using UnityEngine;

namespace RogueDungeon
{
    public class GameRunner : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            var factory = new CharacterFactory(transform);
            _game = new Game(factory);
            _game.CreateCharacter("PlayerConfig", Position.Player);
            _game.CreateCharacter("EnemyConfig", Position.Frontline);
        }

        private void Update() => 
            _game.Tick();
    }
}