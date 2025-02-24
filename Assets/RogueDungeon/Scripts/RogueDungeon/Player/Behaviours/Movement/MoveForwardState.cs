using System.Linq;
using Common.Fsm;
using Common.Unity;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class MoveForwardState : TraversalState, IExitableState
    {
        private readonly IPlayerInput _input;
        private readonly  Level _level;
        private Vector2Int _from;
        private Vector2Int _to;
        protected override float Duration => Config.MoveDuration;
        
        public MoveForwardState(ILevelTraverser levelTraverser, LevelTraverserConfig config, Level level, IPlayerInput input) : base(levelTraverser, config)
        {
            _level = level;
            _input = input;
        }

        public override void Enter()
        {
            base.Enter();
            _input.ConsumeInput(InputKey.MoveForward);
            _from = LevelTraverser.Position.Round();
            _level.Rooms.First(n => n.Coordinates == _from).Exit();
            _to = _from + LevelTraverser.Direction.Round();
        }
        
        public void Exit() => 
            _level.Rooms.First(n => n.Coordinates == LevelTraverser.Position.Round()).Enter();

        protected override void SetValueNormalized(float value)
        {
            var from = GetPositionInTileWithOffset(_from);
            var to = GetPositionInTileWithOffset(_to);
            LevelTraverser.Position = Vector2.Lerp(from, to, value);
        }
    }
}