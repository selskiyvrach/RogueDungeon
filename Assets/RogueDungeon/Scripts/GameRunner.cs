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
            var positions = Resources.Load<CharacterScenePositions>("Configs/Characters/RelativePositions");
            _game = new Game(factory, positions);
            _game.CreateCharacter("PlayerConfig", Position.Player);
            _game.CreateCharacter("EnemyConfig", Position.Frontline);
            _game.CreateCharacter("EnemyConfig", Position.BacklineLeft);
            _game.CreateCharacter("EnemyConfig", Position.BacklineRight);
        }

        private void Update() => 
            _game.Tick();
    }
}