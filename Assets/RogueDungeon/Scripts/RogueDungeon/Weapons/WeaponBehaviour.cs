using Common.Behaviours;
using Common.FSM;
using UnityEngine.Assertions;

namespace RogueDungeon.Weapons
{
    public class WeaponBehaviour : Behaviour
    {
        private readonly IAttackMediator _mediator;
        private readonly IAttackInputProvider _inputProvider;
        private readonly IAttackComboConfig _comboConfig;
        
        private readonly StateMachine _stateMachine;

        public WeaponBehaviour(IAttackMediator mediator, IAttackInputProvider inputProvider, IAttackComboConfig comboConfig)
        {
            _mediator = mediator;
            _inputProvider = inputProvider;
            _comboConfig = comboConfig;

            var attackIdle = new State().OnEnter(() => _mediator.AttackState.Value = AttackState.None);
            
            var prepareAttack = new TimerState(() => GetTimings().GetPrepareDuration()).OnEnter(() => _mediator.AttackState.Value = AttackState.Preparing);
            var executeAttack = new TimerState(() => GetTimings().GetExecuteDuration()).OnEnter(() => _mediator.AttackState.Value = AttackState.Executing);
            
            var finishAttack = new TimerState(() => GetTimings().GetFinishDuration())
                .OnEnter(() => _mediator.AttackState.Value = AttackState.Finishing)
                .OnExit(() => _mediator.ComboIndex = 0);
            
            var attackBuilder = new StateMachineBuilder(attackIdle, prepareAttack, executeAttack, finishAttack);
            var canStartAttack = new If(_mediator.CanStartAttack);
            var shouldStartAttack = new IfAll(new If(_inputProvider.HasAttackInput), canStartAttack);
            
            // basic flow of states
            attackBuilder.AddTransition(attackIdle, prepareAttack, shouldStartAttack);
            attackBuilder.AddTransitionFromFinished(prepareAttack, executeAttack, canStartAttack);
            attackBuilder.AddTransitionFromFinished(executeAttack, finishAttack);
            attackBuilder.AddTransitionFromFinished(finishAttack, attackIdle);
            
            // combo continuation
            attackBuilder.AddTransitionFromFinished(executeAttack, prepareAttack, new If(() => TryStartNextComboAttack(shouldStartAttack)));
            
            // attack cancel by another action
            attackBuilder.AddTransitionFromFinished(prepareAttack, finishAttack, new Not(canStartAttack));
            _stateMachine = attackBuilder.Build();
        }

        private bool TryStartNextComboAttack(ICondition shouldStartAttack)
        {
            if (!shouldStartAttack.IsMet() || _mediator.ComboIndex >= _comboConfig.Count - 1)
                return false;
            _mediator.ComboIndex++;
            return true;
        }

        public override void Enable()
        {
            Assert.IsTrue(_mediator.AttackState.Value == AttackState.None);
            Assert.IsTrue(_mediator.ComboIndex == 0);
            
            base.Enable();
            _stateMachine.Run();
        }

        public override void Disable()
        {
            base.Disable();
            _mediator.AttackState.Value = AttackState.None;
            _mediator.ComboIndex = 0;
            _stateMachine.Stop();
        }

        public override void Tick() => 
            _stateMachine.Tick();

        private IAttackTimingsProvider GetTimings() => 
            _comboConfig.GetTimings(_mediator.ComboIndex);
    }
}