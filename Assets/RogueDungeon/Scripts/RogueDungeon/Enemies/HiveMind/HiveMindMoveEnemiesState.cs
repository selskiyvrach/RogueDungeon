using System.Collections.Generic;
using System.Linq;
using Common.Behaviours;
using Common.Fsm;
using Common.Time;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindMoveEnemiesState : HiveMindState
    {
        private readonly HiveMindContext _context;
        private readonly HiveMindConfig _config;
        private readonly RoomLocalPositionsConfig _positionsConfig;
        
        private readonly List<Movement> _movements = new(3);

        private float _timePassed;

        protected override bool IsSlackFrame => true;

        public HiveMindMoveEnemiesState(HiveMindContext context, HiveMindConfig config, RoomLocalPositionsConfig positionsConfig) : base(context)
        {
            _context = context;
            _config = config;
            _positionsConfig = positionsConfig;
        }

        public override void Enter()
        {
            base.Enter();
            _movements.AddRange(_context.EnemiesToMove.Select(n => new Movement
            {
                Target = n.enemy,
                DestinationCoordinates = _positionsConfig.Get(n.destination),
                StartCoordinates = n.enemy.WorldObject.LocalPosition,
                DestinationPosition = n.destination,
            }));
            
            _context.EnemiesToMove.Clear();
            Assert.IsTrue(_movements.Any());
            
            foreach (var movement in _movements) 
                movement.Target.CombatPosition = EnemyPosition.ChangingPosition;
            
            _timePassed = 0;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _timePassed += timeDelta;
            var normTime = Mathf.Clamp01(_timePassed / _config.MoveFromPositionToPositionDuration);
            foreach (var movement in _movements) 
                movement.Target.WorldObject.LocalPosition = Vector2.Lerp(movement.StartCoordinates, movement.DestinationCoordinates, normTime);
        }

        public override void Exit()
        {
            base.Exit();
            foreach (var n in _movements) 
                n.Target.CombatPosition = n.DestinationPosition;
            _movements.Clear();
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(_timePassed >= _config.MoveFromPositionToPositionDuration)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}