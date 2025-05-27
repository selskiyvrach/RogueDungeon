using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Common.UtilsUnity;
using Enemies.States;
using UnityEngine;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace Enemies
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
        public bool IsIdle { get; set; }
        public bool IsMoving { get; set; }
        public bool IsStunned { get; set; }
        public EnemyMove CurrentMove => _stateMachine.CurrentMove;
        public bool IsStunnedOrDead => !IsAlive || IsStunned;
        public EnemyAttackMoveConfig[] Attacks => Config.Attacks;
        public event Action<EnemyMove, EnemyMove> OnStateChanged { add => _stateMachine.OnStateChanged += value; remove => _stateMachine.OnStateChanged -= value; }
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
            _stateMachine.Initialize(_statesProvider.GetState(MoveNames.IDLE));
            _stateMachine.TryStartState(_statesProvider.GetState(MoveNames.BIRTH));
        }

        public void Destroy() =>
            ((TwoDWorldObject)WorldObject).Destroy();

        public HitSeverity TakeDamage(float damage, float poiseDamage)
        {
            var severity = HitSeverity.Regular;
            if (IsStunned)
            {
                _stateMachine.StartState(_statesProvider.GetState(MoveNames.IDLE));
                damage *= 2;
                severity = HitSeverity.Critical;
            }
            
            Health.AddDelta(-damage);
            Poise.AddDelta(-poiseDamage);
            if (!IsAlive) 
                _stateMachine.TryStartState(_statesProvider.GetState(MoveNames.DEATH));

            if (Poise.Current == 0) 
                _stateMachine.TryStartState(_statesProvider.GetState(MoveNames.STAGGER));

            if(damage > 0 || poiseDamage > 0)
                _impactAnimator.OnHit();
            
            return severity;
        }

        public void ChangePosition(EnemyPosition position)
        {
            var state = (EnemyMovementMove)_statesProvider.GetState(MoveNames.MOVE);
            state.TargetPosition = position;
            _stateMachine.TryStartState(state);
        }

        public bool HasAttacksForCurrentPosition(out IEnumerable<EnemyAttackMoveConfig> moves) => 
            (moves = Attacks.Where(n => (n.SuitableForPositions & OccupiedPosition) != 0)).Any();

        public void StartMove(string name) => 
            _stateMachine.TryStartState(_statesProvider.GetState(name));
    }
}