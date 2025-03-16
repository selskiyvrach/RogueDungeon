using Common.Animations;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerRotationMove : PlayerMove
    {
        private readonly PlayerRotationConfig _rotationConfig;
        private readonly Level _level;
        private float _from;
        private float _to;

        public PlayerRotationMove(Level level, PlayerRotationConfig config, IPlayerInput playerInput, IAnimation animation) : base(config, animation, playerInput)
        {
            _rotationConfig = config;
            _level = level;
        }

        public override void Enter()
        {
            base.Enter();
            _from = _level.LevelTraverser.Rotation.Round().Degrees();
            _to = _from + _rotationConfig.RotationDegrees;
            _to %= 360;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            var angle = Mathf.LerpAngle(_from, _to, Animation.Progress);
            _level.LevelTraverser.Rotation = FromAngle(angle);
        }

        private static Vector2 FromAngle(float degrees) =>
            new(Mathf.Cos(degrees * Mathf.Deg2Rad), Mathf.Sin(degrees * Mathf.Deg2Rad));
    }
}