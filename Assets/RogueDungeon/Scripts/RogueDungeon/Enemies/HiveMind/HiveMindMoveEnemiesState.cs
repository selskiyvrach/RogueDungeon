using System.Collections.Generic;
using System.Linq;
using Common.Behaviours;
using Common.Fsm;
using RogueDungeon.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindMoveEnemiesState : HiveMindState
    {
        private struct Movement
        {
            public Enemy Target;
            public EnemyPosition DestinationPosition;
            public Vector2 StartCoordinates;
            public Vector2 DestinationCoordinates;
        }

        private readonly Ticker _ticker = new();
        private readonly HiveMindContext _context;
        private readonly HiveMindConfig _config;
        private readonly RoomLocalPositionsConfig _positionsConfig;
        
        private readonly List<Movement> _movements = new(3);

        private float _timePassed;

        public HiveMindMoveEnemiesState(HiveMindContext context, HiveMindConfig config, RoomLocalPositionsConfig positionsConfig)
        {
            _context = context;
            _config = config;
            _positionsConfig = positionsConfig;
        }

        public override void Enter()
        {
            base.Enter();
            _movements.AddRange(_context.EnemiesToMove.Select(n => new Movement { 
                Target = n.enemy, 
                DestinationCoordinates = _positionsConfig.Get(n.destination),
                StartCoordinates = new Vector2(n.enemy.GameObject.transform.localPosition.x, n.enemy.GameObject.transform.localPosition.z),
                DestinationPosition = n.destination,
            }));  
            
            _context.EnemiesToMove.Clear();
            Assert.IsTrue(_movements.Any());
            
            foreach (var movement in _movements) 
                movement.Target.CombatPosition = EnemyPosition.ChangingPosition;
            
            _timePassed = 0;
            _ticker.Start(Tick);
        }

        private void Tick(float timeDelta)
        {
            _timePassed += timeDelta;
            var normTime = Mathf.Clamp01(_timePassed / _config.MoveFromPositionToPositionDuration);
            foreach (var movement in _movements)
            {
                var coords = Vector2.Lerp(movement.StartCoordinates, movement.DestinationCoordinates, normTime);
                movement.Target.GameObject.transform.localPosition = new Vector3(coords.x, 0, coords.y); 
            }
        }

        public override void Exit()
        {
            base.Exit();
            foreach (var n in _movements) 
                n.Target.CombatPosition = n.DestinationPosition;
            _movements.Clear();
            _ticker.Stop();
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(_timePassed >= _config.MoveFromPositionToPositionDuration)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}