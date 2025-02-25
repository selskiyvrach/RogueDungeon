using UnityEngine;

namespace RogueDungeon.Levels
{
    public class RoomConfig : ScriptableObject
    {
        [field: SerializeField] public RoomPresenter Prefab { get; private set; }
        [field: SerializeField] public Vector2Int Coordinates { get; private set; }
        [field: SerializeField] public RoomEventConfig[] EventConfigs { get; private set; }
    }
}