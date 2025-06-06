using Game.Libs.Input;
using Libs.Animations;
using UnityEngine;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public class MoveForwardMove : PlayerRoomMovementMove
    {
        private readonly Player _player;
        private readonly ILevelTraverser _levelTraverser;
        private readonly IRoomsInfoProvider _levelRoomsInfoProvider;
        private Vector2Int _from;
        private Vector2Int _to;

        protected override float Duration => _player.Config.MovementActionDuration;
        protected override InputKey RequiredKey => InputKey.MoveForward;
        protected override RequiredState State => RequiredState.DownOrHeld;

        public MoveForwardMove(Player player, ILevelTraverser levelTraverser, IPlayerInput playerInput, IAnimation animation, string id, IRoomsInfoProvider levelRoomsInfoProvider) : base(levelTraverser, id, animation, playerInput)
        {
            _player = player;
            _levelTraverser = levelTraverser;
            _levelRoomsInfoProvider = levelRoomsInfoProvider;
        }
        
        public override void Enter()
        {
            base.Enter();
            _from = _levelTraverser.GridPosition;
            _levelTraverser.OnExitingRoom(_levelTraverser.GridPosition);
            _to = _from + _levelTraverser.GridRotation;
        }
        
        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _levelTraverser.BlendedGridPosition = Vector2.Lerp(_from, _to, Animation.Progress);
            if(!IsFinished)
                return;
            
            _levelTraverser.GridPosition = _to;
            _levelTraverser.OnEnteringRoom(_levelTraverser.GridPosition);
        }

        protected override bool CanTransitionTo()
        {
            var canBase = base.CanTransitionTo();
            var roomExists = _levelRoomsInfoProvider.RoomExists(_levelTraverser.GridPosition + _levelTraverser.GridRotation);
            return canBase && roomExists;
        }
    }
}