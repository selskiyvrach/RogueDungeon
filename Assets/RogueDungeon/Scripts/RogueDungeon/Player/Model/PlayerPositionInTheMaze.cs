using Common.Unity;
using UnityEngine;

namespace RogueDungeon.Player.Model
{
    public class PlayerPositionInTheMaze : ITwoDWorldObject
    {
        private readonly ITwoDWorldObject _targetObject;
        private readonly PlayerConfig _playerConfig;

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

        public PlayerPositionInTheMaze(ITwoDWorldObject targetObject, PlayerConfig playerConfig)
        {
            _targetObject = targetObject;
            _playerConfig = playerConfig;
        }

        private Vector2 GetOffset() => 
            Rotation2D * -_playerConfig.PositionOffsetFromTileCenter;
    }
}