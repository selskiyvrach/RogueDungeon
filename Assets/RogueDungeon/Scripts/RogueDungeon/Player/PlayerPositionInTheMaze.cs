using Common.Unity;
using UnityEngine;

namespace RogueDungeon.Player
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

        public Vector2 Rotation
        {
            get => _targetObject.Rotation;
            set
            {
                var position = LocalPosition;
                _targetObject.Rotation = value;
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
            Rotation * -_playerConfig.PositionOffsetFromTileCenter;
    }
}