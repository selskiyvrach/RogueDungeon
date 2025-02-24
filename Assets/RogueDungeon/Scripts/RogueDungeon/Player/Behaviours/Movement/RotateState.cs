using System;
using Common.Unity;
using RogueDungeon.Input;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class RotateState : TraversalState
    {
        private readonly IPlayerInput _input;
        private Vector2Int _from;
        private Vector2Int _to;
        protected override float Duration => Config.RotationDuration;

        public RotateState(ILevelTraverser levelTraverser, LevelTraverserConfig config, IPlayerInput input) : base(levelTraverser, config) => 
            _input = input;

        public override void Enter()
        {
            base.Enter();
            Rotation rotation;
            if (_input.HasInput(InputKey.TurnLeft))
            {
                _input.ConsumeInput(InputKey.TurnLeft);
                rotation = Rotation.Left;
            }
            else if (_input.HasInput(InputKey.TurnRight))
            {
                _input.ConsumeInput(InputKey.TurnRight);
                rotation = Rotation.Right;
            }
            else if (_input.HasInput(InputKey.TurnAround))
            {
                _input.ConsumeInput(InputKey.TurnAround);
                rotation = Rotation.Around;
            }
            else 
                throw new Exception("Invalid input for RotateState");
            
            _from = LevelTraverser.Direction.Round();
            _to = ((Vector2)_from).Rotate(rotation switch {
                Rotation.Left => 90,
                Rotation.Right => -90,
                Rotation.Around => 180,
                _ => throw new ArgumentOutOfRangeException()
            }).Round();
        }

        protected override void SetValueNormalized(float value)
        {
            var angle = Vector2.SignedAngle(_from, _to) * value;
            LevelTraverser.Direction = LevelTraverser.Direction.Rotate(angle);
            LevelTraverser.Position = GetRealPosition(LevelTraverser.Position.Round());
        }
    }
}