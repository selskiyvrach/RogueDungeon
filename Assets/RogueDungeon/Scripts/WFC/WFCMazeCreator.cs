using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.WFC
{
    [ExecuteInEditMode]
    public class WFCMazeCreator : MonoBehaviour
    {
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        [SerializeField] private Tile[] _tiles;

        private WFC _wfc;
        private Cell[,] _grid;

        private void Awake() => 
            _wfc = new WFC();

        [Button]
        public void FetchTilesFromResources() => 
            _tiles = Resources.LoadAll<Tile>("Configs/WFCTiles");

        [Button]
        public void NextStep()
        {
            _wfc.NextIteration();
            RecreateGrid();
        }

        [Button]
        public void Create()
        {
            Clear();
            _grid = _wfc.CreateGrid(_tiles, _x, _y);
            RecreateGrid();
        }

        private void RecreateGrid()
        {
            var tileObject = Resources.Load<SpriteRenderer>("Prefabs/WFCTiles/WFCTiles_0");
            foreach (var cell in _grid)
            {
                if (cell.Options.Count > 1)
                    continue;
                var tile = Instantiate(tileObject, transform);
                tile.transform.position = new Vector3(cell.X, cell.Y);
                tile.sprite = cell.Options[0].Sprite;
            }
        }

        private void Clear()
        {
            for (var i = transform.childCount - 1; i >= 0; i--) 
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}