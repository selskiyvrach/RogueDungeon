using Common.Animations;
using UnityEngine;

namespace RogueDungeon.Enemies.States
{
    public class EnemyMovementMove : EnemyMove
    {
        private readonly RoomLocalPositionsConfig _positionsConfig;
        private readonly Enemy _enemy;
        private Vector2 _destinationCoordinates;
        private Vector2 _startCoordinates;
        public EnemyPosition TargetPosition { get; set; }
        protected override float Duration => _enemy.Config.ChangePositionDuration;

        protected EnemyMovementMove(IAnimation animation, Enemy enemy, RoomLocalPositionsConfig positionsConfig, string id) : base(animation, id)
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
            _enemy.IsMoving = true;
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.TargetablePosition = _enemy.OccupiedPosition;
            _enemy.IsMoving = false;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _enemy.WorldObject.LocalPosition = Vector2.Lerp(_startCoordinates, _destinationCoordinates, Animation.Progress);
        }

        public override Priority Priority { get; }
    }
}