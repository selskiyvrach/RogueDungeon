using RogueDungeon.Characters;
using RogueDungeon.UI;
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
            var gameUi = Instantiate(Resources.Load<GameUI>("Prefabs/UI/GameUI"));
            _game = new Game(factory, positions, gameUi);
        }

        private void Update() => 
            _game.Tick();
    }
}