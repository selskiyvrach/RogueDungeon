using Common.Behaviours;
using Common.FSM;
using Common.UniRxUtils;
using UnityEngine.Assertions;

namespace RogueDungeon.Weapons
{
    public class WeaponBehaviour : Behaviour
    {
        private readonly IAttackMediator _mediator;
        private readonly IWeaponInputProvider _inputProvider;
        private readonly IWeaponAttackDirectionsProvider _weaponParameters;
        private readonly StateMachine _stateMachine;
        
        private readonly Timer _attackKeyframeTimer = new();

        public WeaponBehaviour(IAttackMediator mediator, IWeaponInputProvider inputProvider, IWeaponAttackDirectionsProvider weaponParameters)
        {
            _mediator = mediator;
            _inputProvider = inputProvider;
            _weaponParameters = weaponParameters;

            var attackIdle = new State()
                .OnEnter(() => _mediator.AttackState.Value = AttackState.None)
                .OnEnter(() => _mediator.AttackIndex = -1);
            
            var prepareAttack = new TimerState(() => _weaponParameters.AttackPrepareDuration).OnEnter(() => _mediator.AttackState.Value = AttackState.Preparing);
            var executeAttack = new TimerState(() => _weaponParameters.AttackExecuteDuration)
                .OnEnter(() => _mediator.AttackIndex = ++_mediator.AttackIndex % _weaponParameters.ComboAttackDirections.Length)
                .OnEnter(() => _mediator.AttackState.Value = AttackState.Executing)
                .OnEnter(() => _attackKeyframeTimer.Start(_weaponParameters.AttackExecuteDuration / 2, _mediator.OnHitKeyframe.OnNext))
                .OnExit(() => _attackKeyframeTimer.Cancel());
            
            var finishAttack = new TimerState(() => _weaponParameters.AttackFinishDuration)
                .OnEnter(() => _mediator.AttackState.Value = AttackState.Finishing)
                .OnExit(() => _mediator.AttackIndex = 0);
            
            var attackBuilder = new StateMachineBuilder(attackIdle, prepareAttack, executeAttack, finishAttack);
            var canStartAttack = new If(_mediator.CanStartAttack);
            var shouldStartAttack = new IfAll(new If(_inputProvider.HasAttackInput), canStartAttack);
            
            // basic flow of states
            attackBuilder.AddTransition(attackIdle, prepareAttack, shouldStartAttack);
            attackBuilder.AddTransitionFromFinished(prepareAttack, executeAttack, canStartAttack);
            attackBuilder.AddTransitionFromFinished(executeAttack, finishAttack);
            attackBuilder.AddTransitionFromFinished(finishAttack, attackIdle);
            
            // combo continuation
            attackBuilder.AddTransitionFromFinished(executeAttack, executeAttack, shouldStartAttack);
            
            // attack cancel by another action
            attackBuilder.AddTransitionFromFinished(prepareAttack, finishAttack, new Not(canStartAttack));
            _stateMachine = attackBuilder.Build();
        }

        public override void Enable()
        {
            Assert.IsTrue(_mediator.AttackState.Value == AttackState.None);
            Assert.IsTrue(_mediator.AttackIndex == -1);
            
            base.Enable();
            _stateMachine.Run();
        }

        public override void Disable()
        {
            base.Disable();
            _mediator.AttackState.Value = AttackState.None;
            _mediator.AttackIndex = -1;
            _stateMachine.Stop();
        }

        public override void Tick() => 
            _stateMachine.Tick();
    }
}