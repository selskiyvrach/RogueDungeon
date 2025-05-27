using UnityEngine;

namespace RogueDungeon.Items.View
{
    public struct TileInfo
    {
        public bool HasUp {get;}
        public bool HasDown {get;}
        public bool HasLeft {get;}
        public bool HasRight {get;}
        public Vector2Int Position {get;}

        public TileInfo(bool hasUp, bool hasDown, bool hasLeft, bool hasRight, Vector2Int position)
        {
            HasUp = hasUp;
            HasDown = hasDown;
            HasLeft = hasLeft;
            HasRight = hasRight;
            Position = position;
        }
    }
}