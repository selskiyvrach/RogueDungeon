using Sirenix.OdinInspector;
using UnityEngine;

namespace Levels
{
    public class LevelConfig : ScriptableObject
    {
        [field: InfoBox("Starting room should have 0;0 coordinates"), SerializeField] public RoomConfig[] Rooms { get; private set; }
    }
}