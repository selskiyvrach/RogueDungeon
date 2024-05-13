using RogueDungeon.WFC;
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
            var grid = new WFC.WFC().CreateGrid(Resources.LoadAll<Tile>("Configs/WFCTiles"));
            var tileObject = Resources.Load<SpriteRenderer>("Prefabs/WFCTiles/WFCTiles_0");
            foreach (var cell in grid)
            {
                var tile = Instantiate(tileObject, transform);
                tile.transform.position = new Vector3(cell.X, cell.Y);
                tile.sprite = cell.Options[0].Sprite;
            }
            // var positions = Resources.Load<CharacterScenePositions>("Configs/Characters/RelativePositions");
            // var factory = new CharacterFactory(transform);
            // _game = new Game(factory, positions);
        }

        // private void Update() => 
        //     _game.Tick();
    }
}