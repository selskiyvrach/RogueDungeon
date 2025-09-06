using Game.Features.Levels.Domain;
using UnityEngine;
using Room = Game.Features.Levels.Infrastructure.View.Room;

namespace Game.Features.Levels.Infrastructure.Configs
{
    public class RoomConfig : ScriptableObject, IRoomConfig
    {
        [field: SerializeField] public string CombatId { get; private set; }
        [field: SerializeField] public Vector2Int Coordinates { get; private set; }
        [field: SerializeField] public Room Prefab { get; private set; }
    }
}