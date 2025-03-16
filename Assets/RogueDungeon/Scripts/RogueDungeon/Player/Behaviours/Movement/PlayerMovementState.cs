using System.Linq;
using Common.Animations;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementState : PlayerMove
    {
        private readonly Level _level;
        private Vector2 _from;
        private Vector2 _to;
        
        public PlayerMovementState(Level level, PlayerMovementConfig config, IPlayerInput playerInput, IAnimation animation) : base(config, animation, playerInput) => 
            _level = level;

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