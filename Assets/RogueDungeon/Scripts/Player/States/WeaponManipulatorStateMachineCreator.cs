using System.Linq;
using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;
using UnityEngine;

namespace RogueDungeon.Player.States
{
    public class WeaponManipulatorStateMachineCreator : MonoBehaviour
    {
        [Header("Attacks")]
        [SerializeField] private FinishableAnimationPlayer _idleToAttackAnimation;
        [SerializeField] private AttackAnimation[] _attackAnimations;
        [SerializeField] private FinishableAnimationPlayer _toIdleAnimation;

        [Header("Block")]
        [SerializeField] private FinishableAnimationPlayer _idleToBlockAnimation;
        [SerializeField] private FinishableAnimationPlayer _blockToIdleAnimation;
        [SerializeField] private AnimationPlayer _holdBlockAnimation;

        private ICommandsProvider _commandsProvider;
        private ICommandsConsumer _commandsConsumer;

        public void Construct(ICommandsProvider commandsProvider, ICommandsConsumer commandsConsumer)
        {
            _commandsProvider = commandsProvider;
            _commandsConsumer = commandsConsumer;
        }
        
        public IFinishableState GetCombo()
        {
            var builder = new StateMachineBuilder();
            
            var idleState = new State {DebugName = "Equipment idle state"};
            builder.AddState(idleState);
            builder.SetStartState(idleState);
            
            // ATTACKS
            var consumeAttackCommandHandler = new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Attack);
            var hasAttackCommandCondition = new HasCommandCondition(Command.Attack, _commandsProvider);
            var hasNoAttackCommandCondition = new ConditionNegator(hasAttackCommandCondition);

            var idleToAttackState = new FinishableState(_idleToAttackAnimation) {DebugName = "Idle to attack state"};
            idleToAttackState.AddStateEnterHandler(new PlayAnimationStateHandler(_idleToAttackAnimation));
            builder.AddState(idleToAttackState);
            
            var toIdleState = new FinishableState(_idleToAttackAnimation) {DebugName = "Attack to idle state"};
            toIdleState.AddStateEnterHandler(new PlayAnimationStateHandler(_idleToAttackAnimation));
            builder.AddState(toIdleState);

            var attackStates = new FinishableState[_attackAnimations.Length];
            for (var i = 0; i < _attackAnimations.Length; i++)
            {
                var anim = _attackAnimations[i];
                var state = attackStates[i] = new FinishableState(anim) {DebugName = $"Attack{i + 1} state"};
                state.AddStateEnterHandler(new PlayAnimationStateHandler(anim));
                state.AddStateEnterHandler(consumeAttackCommandHandler);
                builder.AddState(state);
            }
            
            for (var i = 0; i < attackStates.Length; i++)
            {
                builder.AddTransitionFromFinishedState(attackStates[i], toIdleState, hasNoAttackCommandCondition);
                builder.AddTransitionFromFinishedState(attackStates[i],
                    i + 1 < attackStates.Length ? attackStates[i + 1] : attackStates[0], hasAttackCommandCondition);
            }
            
            builder.AddTransitionFromToState(idleState, idleToAttackState, hasAttackCommandCondition);
            builder.AddTransitionFromFinishedState(idleToAttackState, attackStates[0]);
            builder.AddTransitionFromFinishedState(toIdleState, idleState);
            // ATTACKS END
            
            // BLOCK
            var hasBlockInputCondition = new HasCommandCondition(Command.Block, _commandsProvider);
            var doesNotHaveBlockInputCondition = new ConditionNegator(hasBlockInputCondition);
            
            var idleToBlockState = new FinishableState(_idleToBlockAnimation) {DebugName = "Idle to block state"};
            idleToBlockState.AddStateEnterHandler(new PlayAnimationStateHandler(_idleToBlockAnimation));
            idleToBlockState.AddStateEnterHandler(new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Block));
            var blockToIdleState = new FinishableState(_blockToIdleAnimation){DebugName = "Block to idle state"};
            blockToIdleState.AddStateEnterHandler(new PlayAnimationStateHandler(_blockToIdleAnimation));
            var holdBlockState = new State {DebugName = "Hold block state"};
            holdBlockState.AddStateEnterHandler(new PlayAnimationStateHandler(_holdBlockAnimation));
            
            builder.AddState(idleToBlockState);
            builder.AddState(holdBlockState);
            builder.AddState(blockToIdleState);
            
            builder.AddTransitionFromToState(idleState, idleToBlockState, hasBlockInputCondition);
            builder.AddTransitionFromFinishedState(idleToBlockState, holdBlockState);
            builder.AddTransitionFromToState(holdBlockState, blockToIdleState, doesNotHaveBlockInputCondition);
            builder.AddTransitionFromFinishedState(blockToIdleState, idleState);
            // BLOCK END

            // FINISHING LOGIC
            var finishedToken = new IsFinishedToken();
            idleState.AddStateEnterHandler(new FinishableSetterStateEnterHandler(finishedToken, true));
            idleState.AddStateExitHandler(new FinishableSetterStateExitHandler(finishedToken, false));
            // FINISHING LOGIC END
            
            return new StateMachineToFinishableStateAdapter(builder.Build(), finishedToken) {DebugName = "Weapon manipulation state"};
        }
    }
}