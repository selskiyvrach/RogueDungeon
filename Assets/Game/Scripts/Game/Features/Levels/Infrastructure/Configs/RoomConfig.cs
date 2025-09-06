using System.Linq;
using Game.Features.Levels.Domain;
using Sirenix.OdinInspector;
using UnityEngine;
using Room = Game.Features.Levels.Infrastructure.View.Room;

namespace Game.Features.Levels.Infrastructure.Configs
{
    public class RoomConfig : ScriptableObject, IRoomConfig
    {
        [field: SerializeField, ReadOnly] public Vector2Int Coordinates { get; private set; }
        [field: SerializeField] public string CombatId { get; private set; }
        [field: SerializeField] public Room Prefab { get; private set; }

        private void OnValidate()
        {
            var axis = name.Split('_').Last().Split(';');
            Coordinates = new Vector2Int(int.Parse(axis[0]), int.Parse(axis[1]));
        }
    }
}