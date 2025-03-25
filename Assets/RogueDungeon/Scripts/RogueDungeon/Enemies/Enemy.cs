using System.Collections.Generic;
using System.Linq;
using Common.Unity;
using RogueDungeon.Characters;
using RogueDungeon.Enemies.States;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Assert = UnityEngine.Assertions.Assert;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Enemies
{
    public class Enemy : IInitializable, ITickable
    {
        private readonly EnemyStatesProvider _statesProvider;
        private readonly EnemyStateMachine _stateMachine;
        private readonly EnemyConfig _config;
        private readonly EnemyImpactAnimator _impactAnimator;

        public EnemyPosition TargetablePosition { get; set; }
        public EnemyPosition OccupiedPosition { get; set; }

        public Resource Health { get; }
        public RechargeableResource Poise { get; }
        public float CurrentAggression { get; set; }
        public ITwoDWorldObject WorldObject { get; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsIdle => _stateMachine.CurrentState is EnemyIdleState;
        public EnemyMoveConfig[] Moves => _config.Moves;
        public bool IsDoingMove => Moves.Any(n => _stateMachine.CurrentState.Config == n);
        public bool IsAlive => Health.Current > 0;

        public Enemy(EnemyConfig config, GameObject gameObject, EnemyStateMachine stateMachine, EnemyStatesProvider statesProvider, EnemyImpactAnimator impactAnimator)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            _config = config;
            _stateMachine = stateMachine;
            _statesProvider = statesProvider;
            _impactAnimator = impactAnimator;
            Health = new Resource(config.Health);
            Poise = new RechargeableResource(config.Poise);
            Health.Refill();
            Poise.Refill();
        }

        public void Tick(float deltaTime)
        {
            Poise.Tick(deltaTime);
            _stateMachine.Tick(deltaTime);
            _impactAnimator.Tick(deltaTime);
        }

        public void Initialize()
        {
            TargetablePosition = OccupiedPosition;
            _stateMachine.Initialize(_statesProvider.GetState(_config.IdleState));
            _stateMachine.TryStartState(_statesProvider.GetState(_config.BirthState));
        }

        public void TickBattleHeat(float battleHeatDelta)
        {
            if(!IsDoingMove)
                CurrentAggression += _config.Aggression * battleHeatDelta;
        }

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public void TakeDamage(float damage, float poiseDamage)
        {
            Health.AddDelta(-damage);
            Poise.AddDelta(-poiseDamage);
            if(!IsAlive)
                _stateMachine.TryStartState(_statesProvider.GetState(_config.DeathState));
            if(Poise.Current == 0)
                _stateMachine.TryStartState(_statesProvider.GetState(_config.StaggerState));
            if(damage > 0 || poiseDamage > 0)
                _impactAnimator.OnHit();
        }

        public void ChangePosition(EnemyPosition position)
        {
            var state = (EnemyMovementState)_statesProvider.GetState(_config.MoveState);
            state.TargetPosition = position;
            _stateMachine.TryStartState(state);
        }

        public bool HasMovesForCurrentPosition(out IEnumerable<EnemyMoveConfig> moves)
        {
            moves = Moves.Where(n => (n.SuitableForPositions & OccupiedPosition) != 0);
            return moves.Any();
        }

        public void StartMove(EnemyMoveConfig config)
        {
            Assert.IsTrue(Moves.Contains(config));
            CurrentAggression = 0;
            _stateMachine.TryStartState(_statesProvider.GetState(config));
        }
    }
}