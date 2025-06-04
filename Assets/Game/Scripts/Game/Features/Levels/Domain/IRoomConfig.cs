using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public interface IRoomConfig
    {
        Vector2Int Coordinates { get; }
    }
}