using System;
using Common.Unity;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class RotateState : TraversalState
    {
        private Vector2Int _from;
        private Vector2Int _to;
        protected override float Duration => Config.RotationDuration;
        public Rotation Rotation { get; set; }

        public RotateState(ILevelTraverser levelTraverser, LevelTraverserConfig config) : base(levelTraverser, config)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _from = LevelTraverser.Direction.Round();
            _to = ((Vector2)_from).Rotate(Rotation switch {
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