using System;
using Common.FSM;
using UnityEngine;
using UnityEngine.Assertions;
using Behaviour = Common.Behaviours.Behaviour;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackComboConfig
    {
        int Count { get; }
        IAttackTimingsProvider GetTimings(int attackIndex);
    }

    public class WeaponConfig : ScriptableObject, IAttackComboConfig
    {
        [field: SerializeField] public AttackConfig[] Attacks { get; private set; }
        
        public int Count => Attacks.Length;
        
        public IAttackTimingsProvider GetTimings(int attackIndex) => 
            Attacks[attackIndex];
    }

    [Serializable]
    public class AttackConfig : IAttackTimingsProvider
    {
        [field: SerializeField] public float PrepareDuration { get; private set; } = .5f;
        [field: SerializeField] public AnimationClip PrepareAnimation {get; private set; }
        
        [field: SerializeField] public float ExecuteDuration {get; private set;}= .5f;
        [field: SerializeField] public AnimationClip ExecuteAnimation {get; private set; }
        
        [field: SerializeField] public float FinishDuration {get; private set;}= .5f;
        [field: SerializeField] public AnimationClip FinishAnimation {get; private set; }

        public float GetPrepareDuration() => 
            PrepareDuration;

        public float GetExecuteDuration() => 
            ExecuteDuration;

        public float GetFinishDuration() => 
            FinishDuration;
    }

    public class AttackBehaviour : Behaviour
    {
        private readonly IAttackMediator _mediator;
        private readonly IAttackInputProvider _inputProvider;
        private readonly IAttackComboConfig _comboConfig;
        
        private readonly StateMachine _stateMachine;

        public AttackBehaviour(IAttackMediator mediator, IAttackInputProvider inputProvider, IAttackComboConfig comboConfig)
        {
            _mediator = mediator;
            _inputProvider = inputProvider;
            _comboConfig = comboConfig;

            var attackIdle = new State().OnEnter(() => _mediator.AttackState = AttackState.None);
            
            var prepareAttack = new TimerState(() => GetTimings().GetPrepareDuration()).OnEnter(() => _mediator.AttackState = AttackState.Preparing);
            var executeAttack = new TimerState(() => GetTimings().GetExecuteDuration()).OnEnter(() => _mediator.AttackState = AttackState.Executing);
            
            var finishAttack = new TimerState(() => GetTimings().GetFinishDuration())
                .OnEnter(() => _mediator.AttackState = AttackState.Finishing)
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
            Assert.IsTrue(_mediator.AttackState == AttackState.None);
            Assert.IsTrue(_mediator.ComboIndex == 0);
            
            base.Enable();
            _stateMachine.Run();
        }

        public override void Disable()
        {
            base.Disable();
            _mediator.AttackState = AttackState.None;
            _mediator.ComboIndex = 0;
            _stateMachine.Stop();
        }

        public override void Tick() => 
            _stateMachine.Tick();

        private IAttackTimingsProvider GetTimings() => 
            _comboConfig.GetTimings(_mediator.ComboIndex);
    }
}