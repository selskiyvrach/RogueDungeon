using System.Linq;
using Common.Fsm;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;

namespace PlayerMovement
{
    public class MoveForwardState : TraversalState, IExitableState
    {
        private readonly IPlayerInput _input;
        private readonly Level _level;
        private Vector2Int _from;
        private Vector2Int _to;
        protected override float Duration => Config.MoveDuration;
        
        public MoveForwardState(ITwoDWorldObject levelTraverser, LevelTraverserConfig config, Level level, IPlayerInput input) : base(levelTraverser, config)
        {
            _level = level;
            _input = input;
        }

        public override void Enter()
        {
            base.Enter();
            _input.ConsumeInput(InputKey.MoveForward);
            _from = LevelTraverser.LocalPosition.Round();
            _level.Rooms.First(n => n.Coordinates == _from).Exit();
            _to = _from + LevelTraverser.Rotation.Round();
        }
        
        public void Exit() => 
            _level.Rooms.First(n => n.Coordinates == LevelTraverser.LocalPosition.Round()).Enter();

        protected override void SetValueNormalized(float value)
        {
            var from = GetPositionInTileWithOffset(_from);
            var to = GetPositionInTileWithOffset(_to);
            LevelTraverser.LocalPosition = Vector2.Lerp(from, to, value);
        }
    }
}