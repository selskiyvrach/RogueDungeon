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
            var positions = Resources.Load<CharacterScenePositions>("Configs/Characters/RelativePositions");
            var factory = new CharacterFactory(transform);
            _game = new Game(factory, positions);

        }

        private void Update() => 
            _game.Tick();
    }
}