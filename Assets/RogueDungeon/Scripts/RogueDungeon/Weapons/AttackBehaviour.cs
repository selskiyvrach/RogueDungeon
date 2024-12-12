using System;
using Common.Behaviours;
using Common.FSM;
using Common.ScreenSpaceEffects;

namespace RogueDungeon.Weapons
{
    // sheath\unsheath performs a different class
    // it also enables/disables the equipment

    public class AttackBehaviour : Behaviour, IAttackBehaviour
    {
        private readonly IAttackMediator _mediator;
        private readonly IAttackInputProvider _inputProvider;
        private readonly IAttackDirectionsProvider _weaponParameters;
        
        private readonly StateMachine _stateMachine;
        private readonly Timer _attackKeyframeTimer = new();
        
        private int _attackIndex;

        public IAttackActionsDurationsProvider Durations { get; }
        public ScreenSpaceDirection CurrentAttackDirection { get; private set; }
        public event Action OnPrepareAttackStarted = delegate { };
        public event Action OnExecuteAttackStarted = delegate { };
        public event Action OnHitKeyframe = delegate { };
        public event Action OnIdle = delegate { };
        public event Action OnFinishAttackStarted = delegate { };

        public AttackBehaviour(IAttackMediator mediator, IAttackInputProvider inputProvider, IAttackDirectionsProvider weaponParameters, IAttackActionsDurationsProvider durationsProvider)
        {
            _mediator = mediator;
            _inputProvider = inputProvider;
            _weaponParameters = weaponParameters;
            Durations = durationsProvider;

            var attackIdle = new State()
                .OnEnter(() =>
                {
                    _attackIndex = -1;
                    _mediator.IsAttackInterruptable = true;
                    CurrentAttackDirection = ScreenSpaceDirection.None;
                    OnIdle.Invoke();
                });
            
            var prepareAttack = new TimerState(() => Durations.IdleAttackTransition).OnEnter(() =>
            {
                IncrementCurrentAttack();
                OnPrepareAttackStarted.Invoke();
            });
            
            var executeAttack = new TimerState(() => Durations.AttackExecute)
                .OnEnter(() =>
                {
                    _mediator.IsAttackInterruptable = false;
                    _attackKeyframeTimer.Start(Durations.AttackExecute / 2, OnHitKeyframe.Invoke);
                    OnExecuteAttackStarted.Invoke();
                })
                .OnExit(() =>
                {
                    _attackKeyframeTimer.Cancel();
                    _mediator.IsAttackInterruptable = true;
                });

            var finishAttack = new TimerState(() => Durations.IdleAttackTransition)
                .OnEnter(() => OnFinishAttackStarted.Invoke());
            
            var attackBuilder = new StateMachineBuilder(attackIdle, prepareAttack, executeAttack, finishAttack);
            var canStartAttack = new If(_mediator.CanStartAttack);
            var shouldStartAttack = new IfAll(new If(_inputProvider.HasAttackInput), canStartAttack);
            
            // basic flow of states
            attackBuilder.AddTransition(attackIdle, prepareAttack, shouldStartAttack);
            attackBuilder.AddTransitionFromFinished(prepareAttack, executeAttack, canStartAttack);
            // combo continuation
            attackBuilder.AddTransitionFromFinished(executeAttack, executeAttack, new If(() => TryTransitionToNextCombo(shouldStartAttack)));
            attackBuilder.AddTransitionFromFinished(executeAttack, finishAttack);
            attackBuilder.AddTransitionFromFinished(finishAttack, attackIdle);
            
            
            // attack cancel by another action
            attackBuilder.AddTransitionFromFinished(prepareAttack, finishAttack, new Not(canStartAttack));
            _stateMachine = attackBuilder.Build();
        }

        private void IncrementCurrentAttack()
        {
            _attackIndex = ++_attackIndex % _weaponParameters.ComboAttackDirections.Length;
            CurrentAttackDirection = _weaponParameters.ComboAttackDirections[_attackIndex];
        }

        private bool TryTransitionToNextCombo(IfAll shouldStartAttack)
        {
            if (!shouldStartAttack.IsMet())
                return false;
            IncrementCurrentAttack();
            return true;
        }

        public override void Enable()
        {
            base.Enable();
            _stateMachine.Run();
        }

        public override void Disable()
        {
            base.Disable();
            _stateMachine.Stop();
        }

        public override void Tick() => 
            _stateMachine.Tick();
    }
}