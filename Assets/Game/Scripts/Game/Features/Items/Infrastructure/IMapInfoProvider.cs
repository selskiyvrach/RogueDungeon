using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Items.Infrastructure
{
    public interface IMapInfoProvider
    {
        IEnumerable<TileInfo> GetTiles();
        Vector2Int CurrentPlayerPosition { get; }
        event Action OnPlayerPositionChanged;
    }
}