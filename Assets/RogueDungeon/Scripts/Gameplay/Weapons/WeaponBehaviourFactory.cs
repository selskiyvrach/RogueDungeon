using RogueDungeon.Animations;
using RogueDungeon.Gameplay.InputCommands;
using RogueDungeon.Gameplay.States;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using UniRx;
using Zenject;

namespace RogueDungeon.Gameplay.Weapons
{
    public class WeaponBehaviourFactory : IFactory<WeaponBehaviour>
    {
        private readonly WeaponConfig _config;
        private readonly AnimationTarget _animationTarget;
        private readonly ICommandsProvider _commandsProvider;
        private readonly ICommandsConsumer _commandsConsumer;
        private readonly IEventBus<IAnimationEvent> _animationEvents;

        public WeaponBehaviourFactory(ICommandsProvider commandsProvider, ICommandsConsumer commandsConsumer, IEventBus<IAnimationEvent> animationEvents, WeaponConfig config, AnimationTarget animationTarget)
        {
            _commandsProvider = commandsProvider;
            _commandsConsumer = commandsConsumer;
            _animationEvents = animationEvents;
            _config = config;
            _animationTarget = animationTarget;
        }

        public WeaponBehaviour Create() =>
            new(CreateEnterCondition(), CreateState());

        private IfAnyCondition CreateEnterCondition() =>
            new(new HasCommandCondition(Command.Attack, _commandsProvider),
                new HasCommandCondition(Command.Block, _commandsProvider));

        private IFinishableState CreateState()
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
            var idleToAttackAnimation = new AnimationPlayer(_config.IdleToAttackAnimation, _animationTarget);
            idleToAttackState.AddHandler(new PlayAnimationStateHandler(idleToAttackAnimation));
            builder.AddState(idleToAttackState);

            var toIdleAnimation = new AnimationPlayer(_config.ToIdleAnimation, _animationTarget);
            var toIdleState = new State {DebugName = "Attack to idle state"};
            toIdleState.AddHandler(new PlayAnimationStateHandler(toIdleAnimation));
            builder.AddState(toIdleState);

            var attackStates = new State[_config.AttackAnimations.Length];
            for (var i = 0; i < _config.AttackAnimations.Length; i++)
            {
                var anim = new AnimationPlayer(_config.AttackAnimations[i], _animationTarget);
                var state = attackStates[i] = new State {DebugName = $"Attack{i + 1} state"};
                state.AddHandler(new AnimationEventStateHandler<AttackEvent>(_animationEvents, anim, AnimEventNames.ATTACK_HIT, new AttackEvent()));
                state.AddHandler(new PlayAnimationStateHandler(anim));
                state.AddHandler(consumeAttackCommandHandler);
                builder.AddState(state);
            }
            
            for (var i = 0; i < attackStates.Length; i++)
            {
                var animationFinishedCondition = new AnimationPlayerToFinishableAdapter(new AnimationPlayer(_config.AttackAnimations[i], _animationTarget));
                builder.AddTransitionWhenFinished(attackStates[i], toIdleState, animationFinishedCondition, hasNoAttackCommandCondition);
                builder.AddTransitionWhenFinished(attackStates[i],
                    i + 1 < attackStates.Length ? attackStates[i + 1] : attackStates[0], animationFinishedCondition,  hasAttackCommandCondition);
            }
            
            builder.AddTransition(idleState, idleToAttackState, hasAttackCommandCondition);
            builder.AddTransitionWhenFinished(idleToAttackState, attackStates[0], new AnimationPlayerToFinishableAdapter(idleToAttackAnimation));
            builder.AddTransitionWhenFinished(toIdleState, idleState, new AnimationPlayerToFinishableAdapter(toIdleAnimation));
            // ATTACKS END
            
            // BLOCK
            var hasBlockInputCondition = new HasCommandCondition(Command.Block, _commandsProvider);
            var doesNotHaveBlockInputCondition = new ConditionNegator(hasBlockInputCondition);

            var idleToBlockAnimation = new AnimationPlayer(_config.IdleToBlockAnimation, _animationTarget);
            var idleToBlockState = new State {DebugName = "Idle to block state"};
            idleToBlockState.AddHandler(new PlayAnimationStateHandler(idleToBlockAnimation));
            idleToBlockState.AddHandler(new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Block));
            idleToBlockState.AddHandler(new AnimationEventStateHandler<BlockStateEvent>(_animationEvents, idleToBlockAnimation, AnimEventNames.BLOCK_RAISED, 
                new BlockStateEvent(BlockStateEvent.BlockState.Raised)));

            var blockToIdleAnimation = new AnimationPlayer(_config.BlockToIdleAnimation, _animationTarget);
            var blockToIdleState = new State {DebugName = "Block to idle state"};
            blockToIdleState.AddHandler(new PlayAnimationStateHandler(blockToIdleAnimation));
            blockToIdleState.AddHandler(new ConsumeCommandStateEnterHandler(_commandsConsumer, Command.Block));

            var holdBlockAnimation = new AnimationPlayer(_config.HoldBlockAnimation, _animationTarget);
            var holdBlockState = new State {DebugName = "Hold block state"};
            holdBlockState.AddHandler(new PlayAnimationStateHandler(holdBlockAnimation));
            holdBlockState.AddHandler(new AnimationEventStateHandler<BlockStateEvent>(_animationEvents, idleToBlockAnimation, AnimEventNames.BLOCK_LOWERED, 
                new BlockStateEvent(BlockStateEvent.BlockState.Lowered)));
            
            builder.AddState(idleToBlockState);
            builder.AddState(holdBlockState);
            builder.AddState(blockToIdleState);
            
            builder.AddTransition(idleState, idleToBlockState, hasBlockInputCondition);
            builder.AddTransitionWhenFinished(idleToBlockState, holdBlockState, new AnimationPlayerToFinishableAdapter(idleToBlockAnimation));
            builder.AddTransition(holdBlockState, blockToIdleState, doesNotHaveBlockInputCondition);
            builder.AddTransitionWhenFinished(blockToIdleState, idleState, new AnimationPlayerToFinishableAdapter(blockToIdleAnimation));
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