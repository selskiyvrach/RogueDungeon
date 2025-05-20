using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public struct GridSpaceItem
    {
        public int Id { get; }
        public Vector2Int Size { get; }
        public Vector2Int Position { get; set; }

        public IEnumerable<Vector2Int> CoveredCells
        {
            get
            {
                for (var x = 0; x < Size.x; x++)
                for (var y = 0; y < Size.y; y++)
                    yield return new Vector2Int(Position.x + x, Position.y + y);
            }
        }

        public GridSpaceItem(int id, Vector2Int size, Vector2Int position) : this()
        {
            Id = id;
            Size = size;
            Position = position;
        }
    }
}