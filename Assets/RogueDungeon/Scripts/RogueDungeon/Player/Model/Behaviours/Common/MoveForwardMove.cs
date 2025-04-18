using System.Linq;
using Common.Animations;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class MoveForwardMove : PlayerInputMove
    {
        private readonly IPlayerInput _input;
        private readonly Player _player;
        private readonly Level _level;
        private Vector2 _from;
        private Vector2 _to;

        protected override float Duration => _player.Config.MovementActionDuration;
        protected override InputKey RequiredKey => InputKey.MoveForward;
        protected override RequiredState State => RequiredState.DownOrHeld;

        public MoveForwardMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation, string id, IPlayerInput input) : base(id, animation, playerInput)
        {
            _player = player;
            _level = level;
            _input = input;
        }


        public override void Enter()
        {
            base.Enter();
            _from = _level.LevelTraverser.LocalPosition.Round();
            _level.CurrentRoom.Exit();
            _to = _from + _level.LevelTraverser.Rotation.Round();
        }
        
        public override void Exit()
        {
            base.Exit();
            _level.RefreshCurrentRoom();
            _level.CurrentRoom.Enter();
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _level.LevelTraverser.LocalPosition = Vector2.Lerp(_from, _to, Animation.Progress);
        }

        protected override bool CanTransitionTo() =>
            base.CanTransitionTo() && _level.Rooms
                .First(n => n.Coordinates == _level.LevelTraverser.LocalPosition.Round()).AdjacentRooms
                .HasAdjacentRoom(_level.LevelTraverser.Rotation.Round());
    }
}