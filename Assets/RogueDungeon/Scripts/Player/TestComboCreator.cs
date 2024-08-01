using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;
using UnityEngine;

namespace RogueDungeon.Player.States
{
    public class TestComboCreator : MonoBehaviour
    {
        [SerializeField] private SwingAnimation _idleToAttack1Animation;
        [SerializeField] private AttackAnimation _attack1Animation;
        [SerializeField] private SwingAnimation _attack1ToAttack2Animation;
        [SerializeField] private SwingAnimation _attack1ToIdleAnimation;
        
        [SerializeField] private AttackAnimation _attack2Animation;
        [SerializeField] private SwingAnimation _attack2ToAttack1Animation;
        [SerializeField] private SwingAnimation _attack2ToIdleAnimation;
        
        private ICommandsProvider _commandsProvider;
        private ICommandsConsumer _commandsConsumer;

        // [Inject]
        public void Construct(ICommandsProvider commandsProvider, ICommandsConsumer commandsConsumer)
        {
            _commandsProvider = commandsProvider;
            _commandsConsumer = commandsConsumer;
        }

        public IFinishableState GetCombo()
        {
            var consumeAttackCommandStateHandler = new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Attack);

            var idleState = new IdleState();
            var isFinishedToken = new IsFinishedToken();
            idleState.AddStateEnterHandler(new SetFinishableStateEnterHandler(isFinishedToken));
            idleState.AddStateExitHandler(new SetFinishableStateExitHandler(isFinishedToken, false));
            
            var attack1 = new AttackState(_attack1Animation);
            attack1.AddStateEnterHandler(consumeAttackCommandStateHandler);
            
            var idleToAttack1 = new SwingState(_idleToAttack1Animation);
            var attack1ToIdle = new SwingState(_attack1ToIdleAnimation);
            var attack1ToAttack2 = new SwingState(_attack1ToAttack2Animation);
            var attack2ToAttack1 = new SwingState(_attack2ToAttack1Animation);
            var attack2ToIdle = new SwingState(_attack2ToIdleAnimation);
            
            var attack2 = new AttackState(_attack2Animation);
            attack2.AddStateEnterHandler(consumeAttackCommandStateHandler);

            var hasAttackCondition = new HasInputCondition(_commandsProvider, Command.Attack);
            var doesNotHaveAttackCondition = new Negator(hasAttackCondition);

            var stateMachineBuilder = new StateMachineBuilder();
            stateMachineBuilder.AddState(idleState);
            stateMachineBuilder.AddState(attack1);
            stateMachineBuilder.AddState(attack2);
            stateMachineBuilder.AddState(idleToAttack1);
            stateMachineBuilder.AddState(attack1ToIdle);
            stateMachineBuilder.AddState(attack1ToAttack2);
            stateMachineBuilder.AddState(attack2ToAttack1);
            stateMachineBuilder.AddState(attack2ToIdle);

            stateMachineBuilder.AddTransitionFromToState(idleState, idleToAttack1, hasAttackCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(idleToAttack1, attack1, hasAttackCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(attack1, attack1ToAttack2, hasAttackCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(attack1ToAttack2, attack2, hasAttackCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(attack2, attack2ToAttack1, hasAttackCondition);
            
            stateMachineBuilder.AddTransitionFromFinishedState(attack1, attack1ToIdle, doesNotHaveAttackCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(attack2, attack2ToIdle, doesNotHaveAttackCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(attack1ToIdle, idleState);
            stateMachineBuilder.AddTransitionFromFinishedState(attack2ToIdle, idleState);

            stateMachineBuilder.SetStartState(idleState);
            var stateMachine = stateMachineBuilder.Build();
            
            var adapter = new StateMachineToFinishableStateAdapter(stateMachine, isFinishedToken);
            return adapter;
        }
    }
}