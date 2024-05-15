using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RogueDungeon.Maze
{
    public class Minimap : MonoBehaviour
    {
        [SerializeField] private RectTransform _tilesParent;
        [SerializeField] private Image _tilePrefab;
        
        public void CreateMap(IEnumerable<Tile> tiles)
        {
            var tilesArray = tiles.OrderBy(n => n.Coordinates).ToArray();
            var tile = tilesArray[0];
        }

        public void UpdateCursor(Vector2 pos, Vector2 rot)
        {
            
        }
    }
}