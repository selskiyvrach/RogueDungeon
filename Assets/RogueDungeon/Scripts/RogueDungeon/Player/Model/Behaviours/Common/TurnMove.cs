using Common.Animations;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public abstract class TurnMove : PlayerInputMove
    {
        private readonly Player _player;
        private readonly Level _level;
        private float _from;
        private float _to;
        protected override float Duration => _player.Config.MovementActionDuration;
        protected abstract float RotationDegrees { get; }

        protected TurnMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation, string id) : base(id, animation, playerInput)
        {
            _player = player;
            _level = level;
        }
        
        public override void Enter()
        {
            base.Enter();
            _from = _level.LevelTraverser.Rotation.Round().Degrees();
            _to = _from + RotationDegrees;
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