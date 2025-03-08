using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindMoveEnemiesState : HiveMindState
    {
        private readonly HiveMind _context;
        private readonly HiveMindConfig _config;
        private readonly RoomLocalPositionsConfig _positionsConfig;
        
        private readonly List<Movement> _movements = new(3);

        private float _timePassed;

        public HiveMindMoveEnemiesState(HiveMind context, HiveMindConfig config, RoomLocalPositionsConfig positionsConfig)
        {
            _context = context;
            _config = config;
            _positionsConfig = positionsConfig;
        }

        public override void Enter()
        {
            base.Enter();
            _movements.Clear();
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
                movement.Target.OccupiedPosition = movement.DestinationPosition;

            
            _timePassed = 0;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _context.SlackTime += timeDelta;
            var moveDuration = _config.MoveDuration;
            var halfMoveDuration = _config.MoveDuration / 2;
            var oldNormTime = Mathf.Clamp01(_timePassed / moveDuration);
            
            _timePassed += timeDelta;
            var normTime = Mathf.Clamp01(_timePassed / moveDuration);
            var passingMidPoint = oldNormTime <= halfMoveDuration && normTime >= halfMoveDuration;

            foreach (var movement in _movements)
            {
                movement.Target.WorldObject.LocalPosition = Vector2.Lerp(movement.StartCoordinates, movement.DestinationCoordinates, normTime);
                if(passingMidPoint)
                    movement.Target.TargetablePosition = movement.DestinationPosition;
            }
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(_timePassed >= _config.MoveDuration)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}