using UnityEngine;

namespace Libs.GridSpace
{
    public struct GridSpaceItem
    {
        public string Id { get; }
        public Vector2Int Size { get; }
        public Vector2Int Position { get; }

        public GridSpaceItem(string id, Vector2Int size, Vector2Int position) : this()
        {
            Id = id;
            Size = size;
            Position = position;
        }
    }
}