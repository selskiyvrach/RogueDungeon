using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public class LevelTraverserContext : ILevelTraverser, IRoomsInfoProvider
    {
        private readonly IPlayerInMazeConfig _playerInMazeConfig;
        private Vector2 _blendedGridPosition;
        private Vector2 _realRotation;
        private Vector2 _offsetFromRoomCenter;

        public LevelTraverserContext(IPlayerInMazeConfig playerInMazeConfig) => 
            _playerInMazeConfig = playerInMazeConfig;

        public bool CanLeave { get; set; } = true;
        
        public Vector2 RealPosition => _blendedGridPosition + _offsetFromRoomCenter;

        public Vector2 BlendedGridPosition
        {
            get => _blendedGridPosition;
            set
            {
                if(value == _blendedGridPosition)
                    return;
                _blendedGridPosition = value; 
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
                _offsetFromRoomCenter = _realRotation.normalized * - _playerInMazeConfig.PositionOffsetFromTileCenter;
                OnRealRotationChanged?.Invoke();
                OnRealPositionChanged?.Invoke();
            }
        }

        public Vector2Int GridPosition { get; set; }
        public Vector2Int GridRotation { get; set; }
        public event Action<Vector2Int> OnRoomExited;
        public event Action<Vector2Int> OnRoomEntered;
        public event Action OnRealPositionChanged;
        public event Action OnRealRotationChanged;
        public HashSet<Vector2Int> ExistingRooms { private get; set; }
        
        public void OnExitingRoom(Vector2Int coordinates) => 
            OnRoomExited?.Invoke(coordinates);

        public void OnEnteringRoom(Vector2Int coordinates) => 
            OnRoomEntered?.Invoke(coordinates);

        public bool RoomExists(Vector2Int coordinates) => 
            ExistingRooms.Contains(coordinates);
    }
}