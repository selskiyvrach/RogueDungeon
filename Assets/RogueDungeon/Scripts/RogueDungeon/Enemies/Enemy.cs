using System;
using System.Collections.Generic;
using System.Linq;
using Common.Unity;
using RogueDungeon.Characters;
using RogueDungeon.Enemies.States;
using UnityEngine;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Enemies
{
    public enum HitSeverity
    {
        None,
        Regular,
        Critical,
    }

    public class Enemy : IInitializable, ITickable
    {
        private readonly EnemyStatesProvider _statesProvider;
        private readonly EnemyStateMachine _stateMachine;
        private readonly EnemyImpactAnimator _impactAnimator;

        public EnemyConfig Config { get; }
        public EnemyPosition TargetablePosition { get; set; }
        public EnemyPosition OccupiedPosition { get; set; }
        public EnemyCombo CurrentCombo { get; }
        public Resource Health { get; }
        public RechargeableResource Poise { get; }
        public ITwoDWorldObject WorldObject { get; }
        public bool IsReadyToBeDisposed { get; set; }
        public bool IsIdle => CurrentState is EnemyIdleState;
        public bool IsMoving => CurrentState is EnemyMovementState;
        public bool IsStunned => CurrentState is EnemyStunState;
        public EnemyState CurrentState => _stateMachine.CurrentState;
        public bool IsStunnedOrDead => !IsAlive || IsStunned;
        public EnemyMoveConfig[] Moves => Config.Moves;
        public event Action<EnemyState, EnemyState> OnStateChanged { add => _stateMachine.OnStateChanged += value; remove => _stateMachine.OnStateChanged -= value; }
        public bool IsAlive => Health.Current > 0;

        public Enemy(EnemyConfig config, GameObject gameObject, EnemyStateMachine stateMachine, EnemyStatesProvider statesProvider, EnemyImpactAnimator impactAnimator)
        {  
            WorldObject = new TwoDWorldObject(gameObject);
            Config = config;
            _stateMachine = stateMachine;
            _statesProvider = statesProvider;
            _impactAnimator = impactAnimator;
            Health = new Resource(config.Health);
            Poise = new RechargeableResource(config.Poise);
            CurrentCombo = new EnemyCombo(this);
            Health.Refill();
            Poise.Refill();
        }

        public void Tick(float deltaTime)
        {
            Poise.Tick(deltaTime);
            CurrentCombo.Tick(deltaTime);
            _stateMachine.Tick(deltaTime);
            _impactAnimator.Tick(deltaTime);
        }

        public void Initialize()
        {
            TargetablePosition = OccupiedPosition;
            _stateMachine.Initialize(_statesProvider.GetState(Config.IdleState));
            _stateMachine.TryStartState(_statesProvider.GetState(Config.BirthState));
        }

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public HitSeverity TakeDamage(float damage, float poiseDamage)
        {
            var severity = HitSeverity.Regular;
            if (IsStunned)
            {
                _stateMachine.StartState(_statesProvider.GetState(Config.IdleState));
                damage *= 2;
                severity = HitSeverity.Critical;
            }
            
            Health.AddDelta(-damage);
            Poise.AddDelta(-poiseDamage);
            if (!IsAlive) 
                _stateMachine.TryStartState(_statesProvider.GetState(Config.DeathState));

            if (Poise.Current == 0) 
                _stateMachine.TryStartState(_statesProvider.GetState(Config.StaggerState));

            if(damage > 0 || poiseDamage > 0)
                _impactAnimator.OnHit();
            
            return severity;
        }

        public void ChangePosition(EnemyPosition position)
        {
            var state = (EnemyMovementState)_statesProvider.GetState(Config.MoveState);
            state.TargetPosition = position;
            _stateMachine.TryStartState(state);
        }

        public bool HasMovesForCurrentPosition(out IEnumerable<EnemyMoveConfig> moves) => 
            (moves = Moves.Where(n => (n.SuitableForPositions & OccupiedPosition) != 0)).Any();

        public void StartMove(EnemyMoveConfig config) => 
            _stateMachine.TryStartState(_statesProvider.GetState(config));
    }
}