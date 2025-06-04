using System.Collections.Generic;
using Game.Features.Levels.Domain;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Levels.Infrastructure.Configs
{
    public class LevelConfig : ScriptableObject, ILevelConfig
    {
        [field: InfoBox("Starting room should have 0;0 coordinates"), SerializeField] public RoomConfig[] RoomConfigs { get; private set; }
        public IEnumerable<IRoomConfig> Rooms => RoomConfigs;
    }
}