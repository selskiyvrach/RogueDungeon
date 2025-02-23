using System.Linq;
using Common.Fsm;
using Common.Unity;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class MoveForwardState : TraversalState, IExitableState
    {
        private readonly Level _level;
        private Vector2Int _from;
        private Vector2Int _to;
        protected override float Duration => Config.MoveDuration;
        
        public MoveForwardState(ILevelTraverser levelTraverser, LevelTraverserConfig config, Level level) : base(levelTraverser, config) => 
            _level = level;

        public override void Enter()
        {
            base.Enter();
            _from = LevelTraverser.Position.Round();
            _level.Rooms.First(n => n.Coordinates == _from).Exit();
            _to = _from + LevelTraverser.Direction.Round();
        }
        
        public void Exit() => 
            _level.Rooms.First(n => n.Coordinates == LevelTraverser.Position).Enter();

        protected override void SetValueNormalized(float value) => 
            LevelTraverser.Position = Vector3.Lerp(GetRealPosition(_from), GetRealPosition(_to), value);
    }
}