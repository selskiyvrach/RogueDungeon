using RogueDungeon.Animations;
using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;
using UnityEngine;

namespace RogueDungeon.Player.States
{
    public class WeaponManipulatorStateMachineCreator : MonoBehaviour
    {
        [Header("Attacks")]
        [SerializeField] private AnimationPlayer _idleToAttackAnimation;
        [SerializeField] private AnimationPlayer[] _attackAnimations;
        [SerializeField] private AnimationPlayer _toIdleAnimation;

        [Header("Block")]
        [SerializeField] private AnimationPlayer _idleToBlockAnimation;
        [SerializeField] private AnimationPlayer _blockToIdleAnimation;
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

            var idleToAttackState = new State {DebugName = "Idle to attack state"};
            idleToAttackState.AddHandler(new PlayAnimationStateHandler(_idleToAttackAnimation));
            builder.AddState(idleToAttackState);
            
            var toIdleState = new State {DebugName = "Attack to idle state"};
            toIdleState.AddHandler(new PlayAnimationStateHandler(_toIdleAnimation));
            builder.AddState(toIdleState);

            var attackStates = new State[_attackAnimations.Length];
            for (var i = 0; i < _attackAnimations.Length; i++)
            {
                var anim = _attackAnimations[i];
                var state = attackStates[i] = new State {DebugName = $"Attack{i + 1} state"};
                state.AddHandler(new PlayAnimationStateHandler(anim));
                state.AddHandler(consumeAttackCommandHandler);
                builder.AddState(state);
            }
            
            for (var i = 0; i < attackStates.Length; i++)
            {
                var animationFinishedCondition = new AnimationPlayerToFinishableAdapter(_attackAnimations[i]);
                builder.AddTransitionWhenFinished(attackStates[i], toIdleState, animationFinishedCondition, hasNoAttackCommandCondition);
                builder.AddTransitionWhenFinished(attackStates[i],
                    i + 1 < attackStates.Length ? attackStates[i + 1] : attackStates[0], animationFinishedCondition,  hasAttackCommandCondition);
            }
            
            builder.AddTransitionFromToState(idleState, idleToAttackState, hasAttackCommandCondition);
            builder.AddTransitionWhenFinished(idleToAttackState, attackStates[0], new AnimationPlayerToFinishableAdapter(_idleToAttackAnimation));
            builder.AddTransitionWhenFinished(toIdleState, idleState, new AnimationPlayerToFinishableAdapter(_toIdleAnimation));
            // ATTACKS END
            
            // BLOCK
            var hasBlockInputCondition = new HasCommandCondition(Command.Block, _commandsProvider);
            var doesNotHaveBlockInputCondition = new ConditionNegator(hasBlockInputCondition);
            
            var idleToBlockState = new State {DebugName = "Idle to block state"};
            idleToBlockState.AddHandler(new PlayAnimationStateHandler(_idleToBlockAnimation));
            idleToBlockState.AddHandler(new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Block));
            var blockToIdleState = new State {DebugName = "Block to idle state"};
            blockToIdleState.AddHandler(new PlayAnimationStateHandler(_blockToIdleAnimation));
            var holdBlockState = new State {DebugName = "Hold block state"};
            holdBlockState.AddHandler(new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Block));
            holdBlockState.AddHandler(new PlayAnimationStateHandler(_holdBlockAnimation));
            
            builder.AddState(idleToBlockState);
            builder.AddState(holdBlockState);
            builder.AddState(blockToIdleState);
            
            builder.AddTransitionFromToState(idleState, idleToBlockState, hasBlockInputCondition);
            builder.AddTransitionWhenFinished(idleToBlockState, holdBlockState, new AnimationPlayerToFinishableAdapter(_idleToBlockAnimation));
            builder.AddTransitionFromToState(holdBlockState, blockToIdleState, doesNotHaveBlockInputCondition);
            builder.AddTransitionWhenFinished(blockToIdleState, idleState, new AnimationPlayerToFinishableAdapter(_blockToIdleAnimation));
            // BLOCK END

            // FINISHING LOGIC
            var finishedToken = new IsFinishedToken();
            idleState.AddHandler(new FinishableSetterStateEnterHandler(finishedToken, true));
            idleState.AddHandler(new FinishableSetterStateExitHandler(finishedToken, false));
            // FINISHING LOGIC END
            
            return new StateMachineToFinishableStateAdapter(builder.Build(), finishedToken) {DebugName = "Weapon manipulation state"};
        }
    }
}