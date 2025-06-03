using Game.Libs.WorldObjects;
using UnityEngine;

namespace Game.Features.Player.Domain
{
    public class PlayerPositionInTheMaze : ITwoDWorldObject
    {
        private readonly ITwoDWorldObject _targetObject;
        private readonly IPlayerInMazeConfig _playerConfig;

        public Vector2 LocalPosition
        {
            get => _targetObject.LocalPosition - GetOffset();
            set
            {
                var offset = GetOffset();
                _targetObject.LocalPosition = value + offset;
            }
        }

        public Vector2 Rotation2D
        {
            get => _targetObject.Rotation2D;
            set
            {
                var position = LocalPosition;
                _targetObject.Rotation2D = value;
                // refresh position
                LocalPosition = position;
            }
        }

        public PlayerPositionInTheMaze(ITwoDWorldObject targetObject, IPlayerInMazeConfig playerConfig)
        {
            _targetObject = targetObject;
            _playerConfig = playerConfig;
        }

        private Vector2 GetOffset() => 
            Rotation2D * -_playerConfig.PositionOffsetFromTileCenter;
    }
}