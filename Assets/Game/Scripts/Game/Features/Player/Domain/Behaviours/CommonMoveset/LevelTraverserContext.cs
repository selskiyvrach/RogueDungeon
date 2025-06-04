using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class LevelTraverserContext : ILevelTraverser, IRoomsInfoProvider
    {
        private Vector2 _realPosition;
        private Vector2 _realRotation;
        public bool CanLeave { get; set; } = true;

        public Vector2 RealPosition
        {
            get => _realPosition;
            set
            {
                if(value == _realPosition)
                    return;
                _realPosition = value; 
                OnRealPositionChanged?.Invoke();
            }
        }

        public Vector2 RealRotation
        {
            get => _realRotation;
            set
            {
                if(value == _realRotation)
                    return;
                _realRotation = value;
                OnRealRotationChanged?.Invoke();
            }
        }

        public Vector2Int GridPosition { get; set; }
        public Vector2Int GridRotation { get; set; }
        public event Action<Vector2Int> OnRoomExited;
        public event Action<Vector2Int> OnRoomEntered;
        public event Action OnRealPositionChanged;
        public event Action OnRealRotationChanged;
        public HashSet<Vector2Int> ExistingRooms { private get; set; }
        
        public void OnLeavingRoom(Vector2Int coordinates) => 
            OnRoomExited?.Invoke(coordinates);

        public void OnEnteringRoom(Vector2Int coordinates) => 
            OnRoomEntered?.Invoke(coordinates);

        public bool RoomExists(Vector2Int coordinates) => 
            ExistingRooms.Contains(coordinates);
    }
}