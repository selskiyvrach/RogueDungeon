using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Items.View
{
    public interface IMapInfoProvider
    {
        IEnumerable<TileInfo> GetTiles();
        Vector2Int CurrentPlayerPosition { get; }
        event Action OnPlayerPositionChanged;
    }
}