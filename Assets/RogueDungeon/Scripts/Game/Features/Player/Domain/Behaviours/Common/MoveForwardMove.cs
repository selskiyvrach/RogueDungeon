using System.Linq;
using Game.Features.Levels;
using Game.Features.Levels.Domain;
using Game.Libs.Input;
using Libs.Animations;
using UnityEngine;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class MoveForwardMove : PlayerRoomMovementMove
    {
        private readonly PlayerModel _player;
        private readonly Level _level;
        private Vector2 _from;
        private Vector2 _to;

        protected override float Duration => _player.Config.MovementActionDuration;
        protected override InputKey RequiredKey => InputKey.MoveForward;
        protected override RequiredState State => RequiredState.DownOrHeld;

        public MoveForwardMove(PlayerModel player, Level level, IPlayerInput playerInput, IAnimation animation, string id) : base(level, id, animation, playerInput)
        {
            _player = player;
            _level = level;
        }
        
        public override void Enter()
        {
            base.Enter();
            _from = _level.LevelTraverser.LocalPosition.Round();
            _level.CurrentRoom.Exit();
            _to = _from + _level.LevelTraverser.Rotation2D.Round();
        }
        
        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _level.LevelTraverser.LocalPosition = Vector2.Lerp(_from, _to, Animation.Progress);
            if(!IsFinished)
                return;
            
            _level.RefreshCurrentRoom();
            _level.CurrentRoom.Enter();
        }

        protected override bool CanTransitionTo() =>
            base.CanTransitionTo() && _level.Rooms
                .First(n => n.Coordinates == _level.LevelTraverser.LocalPosition.Round()).AdjacentRooms
                .HasAdjacentRoom(_level.LevelTraverser.Rotation2D.Round());
    }
}