using Common.Animations;
using UnityEngine;

namespace RogueDungeon.Enemies.States
{
    public class EnemyMoveState : EnemyState
    {
        private readonly RoomLocalPositionsConfig _positionsConfig;
        private readonly Enemy _enemy;
        private Vector2 _destinationCoordinates;
        private Vector2 _startCoordinates;
        public EnemyPosition TargetPosition { get; set; }

        protected EnemyMoveState(EnemyStateConfig config, IAnimation animation, Enemy enemy, RoomLocalPositionsConfig positionsConfig) : base(config, animation)
        {
            _enemy = enemy;
            _positionsConfig = positionsConfig;
        }

        public override void Enter()
        {
            base.Enter();
            _destinationCoordinates = _positionsConfig.Get(TargetPosition);
            _startCoordinates = _enemy.WorldObject.LocalPosition;
            _enemy.OccupiedPosition = TargetPosition;
            _enemy.TargetablePosition = EnemyPosition.None;
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.TargetablePosition = _enemy.OccupiedPosition;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _enemy.WorldObject.LocalPosition = Vector2.Lerp(_startCoordinates, _destinationCoordinates, Animation.Progress);
        }
    }
}