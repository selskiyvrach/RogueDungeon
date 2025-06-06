using Libs.Animations;
using Libs.Movesets;
using UnityEngine;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public class BirthMove : Move
    {
        private readonly ILevelTraverser _levelTraverser;
        private readonly Player _player;
        protected override float Duration => _player.Config.BirthAnimationDuration;

        public BirthMove(string id, IAnimation animation, Player player, ILevelTraverser levelTraverser) : base(id, animation)
        {
            _player = player;
            _levelTraverser = levelTraverser;
        }

        public override void Enter()
        {
            base.Enter();
            _levelTraverser.GridRotation = Vector2Int.up;
            _levelTraverser.RealRotation = Vector2.up;
        }
    }
}