using System;
using Common.Unity;
using RogueDungeon.Input;
using UnityEngine;

namespace PlayerMovement
{
    public class RotateState : TraversalState
    {
        private readonly IPlayerInput _input;
        private float _from;
        private float _to;
        protected override float Duration => Config.RotationDuration;

        public RotateState(ITwoDWorldObject levelTraverser, LevelTraverserConfig config, IPlayerInput input) : base(levelTraverser, config) => 
            _input = input;

        public override void Enter()
        {
            base.Enter();
            _from = LevelTraverser.Rotation.Round().Degrees();
            _to = _from;
            if (_input.HasInput(InputKey.TurnLeft))
            {
                _input.ConsumeInput(InputKey.TurnLeft);
                _to += 90;
            }
            else if (_input.HasInput(InputKey.TurnRight))
            {
                _input.ConsumeInput(InputKey.TurnRight);
                _to += -90;
            }
            else if (_input.HasInput(InputKey.TurnAround))
            {
                _input.ConsumeInput(InputKey.TurnAround);
                _to += 180;
            }
            else 
                throw new Exception("Invalid input for RotateState");
            _to %= 360;
        }

        protected override void SetValueNormalized(float value)
        {
            var angle = Mathf.LerpAngle(_from, _to, Mathf.Clamp01(value));
            LevelTraverser.Rotation = FromAngle(angle);
            LevelTraverser.LocalPosition = GetPositionInTileWithOffset(LevelTraverser.LocalPosition.Round());
        }

        private static Vector2 FromAngle(float degrees) =>
            new(Mathf.Cos(degrees * Mathf.Deg2Rad), Mathf.Sin(degrees * Mathf.Deg2Rad));
    }
}