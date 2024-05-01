using RogueDungeon.Characters;
using RogueDungeon.Data;
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
            var positions = Resources.Load<CharacterScenePositions>("Configs/Characters/RelativePositions");
            var standardValues = Resources.Load<StandardValues>("Configs/Data/StandardValues");
            var factory = new CharacterFactory(transform, standardValues);
            _game = new Game(factory, positions);
            _game.CreateCharacter("test-char_a", Positions.Frontline);
            _game.CreateCharacter("test-char_a", Positions.BacklineLeft);
            _game.CreateCharacter("test-player", Positions.Player);
        }

        private void Update() => 
            _game.Tick();
    }
}