using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public class RoomConfig : ScriptableObject
    {
        [field: SerializeField] public RoomGameObject Prefab { get; private set; }
        [field: SerializeField] public Vector2Int Coordinates { get; private set; }
    }
}