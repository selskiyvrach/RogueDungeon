using System;
using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public class Room
    {
        private readonly IRoomConfig _config;

        public Vector2Int Coordinates => _config.Coordinates;
        public RoomAdjacencyInfo AdjacencyInfo { get; }
        public bool CanLeave { get; set; }
        public string CombatId => _config.CombatId;
        public event Action<Room> OnEntered;
        
        public Room(IRoomConfig config, RoomAdjacencyInfo adjacencyInfo)
        {
            _config = config;
            AdjacencyInfo = adjacencyInfo;
        }

        public void Enter() => 
            OnEntered?.Invoke(this);

        public void Exit()
        {
        }
    }
}