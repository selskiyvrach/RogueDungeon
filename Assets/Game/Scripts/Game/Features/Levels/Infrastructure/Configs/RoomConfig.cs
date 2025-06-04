using Game.Features.Levels.Domain;
using UnityEngine;

namespace Game.Features.Levels.Infrastructure.Configs
{
    public class RoomConfig : ScriptableObject, IRoomConfig
    {
        [field: SerializeField] public Vector2Int Coordinates { get; private set; }
        [field: SerializeField] public View.Room Prefab { get; private set; }
    }
}