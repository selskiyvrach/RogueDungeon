using Game.Libs.Input;
using Libs.Animations;
using Libs.Utils.Unity;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public abstract class TurnMove : PlayerRoomMovementMove
    {
        private readonly Player _player;
        private readonly ILevelTraverser _levelTraverser;
        private float _from;
        private float _to;
        protected override float Duration => _player.Config.MovementActionDuration;
        protected abstract float RotationDegrees { get; }
        protected override RequiredState State => RequiredState.DownOrHeld;

        protected TurnMove(Player player, ILevelTraverser levelTraverser, IPlayerInput playerInput, IAnimation animation, string id) : base(levelTraverser, id, animation, playerInput)
        {
            _player = player;
            _levelTraverser = levelTraverser;
        }
        
        public override void Enter()
        {
            base.Enter();
            _from = _levelTraverser.GridRotation.Degrees();
            _to = _from + RotationDegrees;
            _to %= 360;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            var angle = Mathf.LerpAngle(_from, _to, Animation.Progress);
            _levelTraverser.RealRotation = FromAngle(angle);
        }

        private static Vector2 FromAngle(float degrees) =>
            new(Mathf.Cos(degrees * Mathf.Deg2Rad), Mathf.Sin(degrees * Mathf.Deg2Rad));
    }
}