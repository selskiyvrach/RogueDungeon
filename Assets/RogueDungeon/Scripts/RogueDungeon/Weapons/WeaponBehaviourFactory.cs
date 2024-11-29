using Common.Events;
using Common.FSM;
using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.Characters;
using RogueDungeon.Player;
using RogueDungeon.PlayerInputCommands;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class WeaponBehaviourFactory : IFactory<WeaponConfig, IWeapon, WeaponBehaviour>
    {
        private readonly IRootObject<AnimationPlayer> _animationTarget;
        private readonly IPlayerInput _playerInput;
        private readonly IEventBus<IAnimationEvent> _animationEvents;

        public WeaponBehaviourFactory(
            IPlayerInput playerInput, 
            IEventBus<IAnimationEvent> animationEvents, 
            IRootObject<AnimationPlayer> animationTarget)
        {
            _playerInput = playerInput;
            _animationEvents = animationEvents;
            _animationTarget = animationTarget;
        }

        public WeaponBehaviour Create(WeaponConfig config, IWeapon weapon) =>
            new(CreateEnterCondition(), CreateState(config, weapon));

        private IfAny CreateEnterCondition() =>
            new(new HasCommand(Command.Attack, _playerInput),
                new HasCommand(Command.Block, _playerInput));

        private IFinishableState CreateState(WeaponConfig config, IWeapon weapon)
        {
            var builder = new StateMachineBuilder();
            
            var idleState = new State {DebugName = "Equipment idle state"};
            builder.AddState(idleState);
            builder.SetStartState(idleState);
            
            // ATTACKS
            var consumeAttackCommandHandler = new ConsumeCommandStateEnterHandler(_playerInput, Command.Attack);
            var hasAttackCommandCondition = new HasCommand(Command.Attack, _playerInput);
            var hasNoAttackCommandCondition = new Not(hasAttackCommandCondition);

            var idleToAttackState = new State {DebugName = "Idle to attack state"};
            var idleToAttackAnimation = new AnimationPlayer(config.IdleToAttackAnimation, _animationTarget);
            idleToAttackState.AddHandler(new PlayAnimationStateHandler(idleToAttackAnimation));
            builder.AddState(idleToAttackState);

            var toIdleAnimation = new AnimationPlayer(config.ToIdleAnimation, _animationTarget);
            var toIdleState = new State {DebugName = "Attack to idle state"};
            toIdleState.AddHandler(new PlayAnimationStateHandler(toIdleAnimation));
            builder.AddState(toIdleState);

            var attackStates = new State[config.AttackAnimations.Length];
            for (var i = 0; i < config.AttackAnimations.Length; i++)
            {
                var anim = new AnimationPlayer(config.AttackAnimations[i], _animationTarget);
                var state = attackStates[i] = new State {DebugName = $"Attack{i + 1} state"};
                state.AddHandler(new AnimationEventStateHandler<AttackEvent>(_animationEvents, anim, AnimEventNames.ATTACK_HIT, new AttackEvent()));
                state.AddHandler(new PlayAnimationStateHandler(anim));
                state.AddHandler(consumeAttackCommandHandler);
                builder.AddState(state);
            }
            
            for (var i = 0; i < attackStates.Length; i++)
            {
                var animationFinishedCondition = new AnimationToFinishable(new AnimationPlayer(config.AttackAnimations[i], _animationTarget));
                builder.AddTransitionFromFinished(attackStates[i], toIdleState, animationFinishedCondition, hasNoAttackCommandCondition);
                builder.AddTransitionFromFinished(attackStates[i],
                    i + 1 < attackStates.Length ? attackStates[i + 1] : attackStates[0], animationFinishedCondition,  hasAttackCommandCondition);
            }
            
            builder.AddTransition(idleState, idleToAttackState, hasAttackCommandCondition);
            builder.AddTransitionFromFinished(idleToAttackState, attackStates[0], new AnimationToFinishable(idleToAttackAnimation));
            builder.AddTransitionFromFinished(toIdleState, idleState, new AnimationToFinishable(toIdleAnimation));
            // ATTACKS END
            
            // BLOCK
            var hasBlockInputCondition = new HasCommand(Command.Block, _playerInput);
            var doesNotHaveBlockInputCondition = new Not(hasBlockInputCondition);

            var idleToBlockAnimation = new AnimationPlayer(config.IdleToBlockAnimation, _animationTarget);
            var idleToBlockState = new State {DebugName = "Idle to block state"};
            idleToBlockState.AddHandler(new PlayAnimationStateHandler(idleToBlockAnimation));
            idleToBlockState.AddHandler(new ConsumeCommandStateEnterHandler(_playerInput, Command.Block));
            idleToBlockState.AddHandler(new AnimationEventStateHandler<BlockEvent>(_animationEvents, idleToBlockAnimation, AnimEventNames.BLOCK_RAISED, 
                new BlockEvent(weapon)));

            var blockToIdleAnimation = new AnimationPlayer(config.BlockToIdleAnimation, _animationTarget);
            var blockToIdleState = new State {DebugName = "Block to idle state"};
            blockToIdleState.AddHandler(new PlayAnimationStateHandler(blockToIdleAnimation));
            blockToIdleState.AddHandler(new ConsumeCommandStateEnterHandler(_playerInput, Command.Block));

            var holdBlockAnimation = new AnimationPlayer(config.HoldBlockAnimation, _animationTarget);
            var holdBlockState = new State {DebugName = "Hold block state"};
            holdBlockState.AddHandler(new PlayAnimationStateHandler(holdBlockAnimation));
            holdBlockState.AddHandler(new AnimationEventStateHandler<BlockEvent>(_animationEvents, idleToBlockAnimation, AnimEventNames.BLOCK_LOWERED, 
                new BlockEvent(null)));
            
            builder.AddState(idleToBlockState);
            builder.AddState(holdBlockState);
            builder.AddState(blockToIdleState);
            
            builder.AddTransition(idleState, idleToBlockState, hasBlockInputCondition);
            builder.AddTransitionFromFinished(idleToBlockState, holdBlockState, new AnimationToFinishable(idleToBlockAnimation));
            builder.AddTransition(holdBlockState, blockToIdleState, doesNotHaveBlockInputCondition);
            builder.AddTransitionFromFinished(blockToIdleState, idleState, new AnimationToFinishable(blockToIdleAnimation));
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