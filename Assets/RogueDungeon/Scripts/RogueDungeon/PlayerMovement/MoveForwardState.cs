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
        private Vector2 _from;
        private Vector2 _to;
        protected override float Duration => Config.MoveDuration;
        
        public MoveForwardState(Level level, LevelTraverserConfig config, IPlayerInput input) : base(level, config)
        {
            _level = level;
            _input = input;
        }

        public override void Enter()
        {
            base.Enter();
            _input.ConsumeInput(InputKey.MoveForward);
            _from = LevelTraverser.LocalPosition.Round();
            _level.CurrentRoom.Exit();
            _to = _from + LevelTraverser.Rotation.Round();
        }
        
        public void Exit()
        {
            _level.RefreshCurrentRoom();
            _level.CurrentRoom.Enter();
        }

        protected override void SetValueNormalized(float value) => 
            LevelTraverser.LocalPosition = Vector2.Lerp(_from, _to, value);
    }
}