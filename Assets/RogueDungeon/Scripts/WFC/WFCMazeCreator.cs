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

        [Button]
        public void FetchTilesFromResources() => 
            _tiles = Resources.LoadAll<Tile>("Configs/WFCTiles");

        [Button]
        public void Create()
        {
            _wfc = new WFC();
            _grid = _wfc.CreateGrid(_tiles, _x, _y);
            Clear();
            RecreateGrid();
        }

        private void RecreateGrid()
        {
            var tileObject = Resources.Load<SpriteRenderer>("Prefabs/WFCTiles/WFCTiles_0");
            foreach (var cell in _grid)
            {
                if (cell.TileOptions.Count > 1)
                    continue;
                var tile = Instantiate(tileObject, transform);
                tile.transform.position = new Vector3(cell.X, cell.Y);
                tile.sprite = cell.TileOptions[0].Sprite;
            }
        }

        private void Clear()
        {
            for (var i = transform.childCount - 1; i >= 0; i--) 
                DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}