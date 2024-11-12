using RogueDungeon.Animations;
using RogueDungeon.Gameplay.InputCommands;
using RogueDungeon.Gameplay.States;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using Zenject;

namespace RogueDungeon.Gameplay
{
    public class PlayerBehaviourStateMachineFactory : IFactory<StateMachine>
    {
        private readonly PlayerInput _playerInput;
        private readonly PlayerAnimationsConfig _animationsConfig;
        private readonly CharacterAnimationRoot _animationRoot;
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private readonly IItemManipulatorProvider _itemManipulatorProvider;

        public PlayerBehaviourStateMachineFactory(
            PlayerInput playerInput, 
            PlayerAnimationsConfig animationsConfig, 
            CharacterAnimationRoot animationRoot, 
            IEventBus<IAnimationEvent> animationEvents, 
            IItemManipulatorProvider itemManipulatorProvider)
        {
            _playerInput = playerInput;
            _animationsConfig = animationsConfig;
            _animationRoot = animationRoot;
            _animationEvents = animationEvents;
            _itemManipulatorProvider = itemManipulatorProvider;
        }

        public StateMachine Create()
        {
            var idleState = CreateIdleState();
            var walkState = CreateWalkState();
            var dodgeRightState = CreateDodgeRightState(out var dodgeRightAnimation);
            var dodgeLeftState = CreateDodgeLeftState(out var dodgeLeftAnimation);
            var itemManipulatorState = new EquipmentManipulationState(_itemManipulatorProvider);
        
            var hasWalkInputCondition = new HasInputCondition(_playerInput, Command.MoveForward);
            var doesNotHaveWalkInputCondition = new ConditionNegator(hasWalkInputCondition);
            var hasDodgeRightInputCondition = new HasInputCondition(_playerInput, Command.DodgeRight);
            var hasDodgeLeftInputCondition = new HasInputCondition(_playerInput, Command.DodgeLeft);

            var builder = new StateMachineBuilder();
            builder.AddState(walkState);
            builder.AddState(idleState);
            builder.AddState(dodgeRightState);
            builder.AddState(dodgeLeftState);
            builder.SetStartState(idleState);
        
            builder.AddTransition(idleState, walkState, hasWalkInputCondition);
            builder.AddTransition(walkState, idleState, doesNotHaveWalkInputCondition);
            
            builder.AddTransition(idleState, dodgeRightState, hasDodgeRightInputCondition);
            builder.AddTransitionWhenFinished(dodgeRightState, idleState, new AnimationPlayerToFinishableAdapter(dodgeRightAnimation));
            
            builder.AddTransition(idleState, dodgeLeftState, hasDodgeLeftInputCondition);
            builder.AddTransitionWhenFinished(dodgeLeftState, idleState, new AnimationPlayerToFinishableAdapter(dodgeLeftAnimation));
            
            builder.SetDebugName("Player root state machine");
            
            builder.AddState(itemManipulatorState);
            
            builder.AddTransition(idleState, itemManipulatorState, itemManipulatorState);
            builder.AddTransition(idleState, itemManipulatorState, itemManipulatorState);
            builder.AddTransitionWhenFinished(itemManipulatorState, idleState, itemManipulatorState);

            return builder.Build();
        }

        private State CreateDodgeLeftState(out AnimationPlayer dodgeLeftAnimation)
        {
            var dodgeLeftState = new State {DebugName = "Dodge left state"};
            dodgeLeftAnimation = new AnimationPlayer(_animationsConfig.DodgeLeftAnimation, _animationRoot);
            dodgeLeftState.AddHandler(new PlayAnimationStateHandler(dodgeLeftAnimation));
            dodgeLeftState.AddHandler(new ConsumeCommandStateEnterHandler(_playerInput, Command.DodgeLeft));
            dodgeLeftState.AddHandler(new ThrowAnimationEventStateHandler<DodgeStartedEvent,DodgeEndedEvent>(
                dodgeLeftAnimation, _animationEvents, AnimEventNames.DODGE_STARTED, AnimEventNames.DODGE_FINISHED));
            return dodgeLeftState;
        }

        private State CreateDodgeRightState(out AnimationPlayer dodgeRightAnimation)
        {
            var dodgeRightState = new State {DebugName = "Dodge right state"};
            dodgeRightAnimation = new AnimationPlayer(_animationsConfig.DodgeRightAnimation, _animationRoot);
            dodgeRightState.AddHandler(new PlayAnimationStateHandler(dodgeRightAnimation));
            dodgeRightState.AddHandler(new ConsumeCommandStateEnterHandler(_playerInput, Command.DodgeRight));
            dodgeRightState.AddHandler(new ThrowAnimationEventStateHandler<DodgeStartedEvent,DodgeEndedEvent>(
                dodgeRightAnimation, _animationEvents, AnimEventNames.DODGE_STARTED, AnimEventNames.DODGE_FINISHED));
            return dodgeRightState;
        }

        private State CreateWalkState()
        {
            var walkState = new State {DebugName = "Walk state"};
            walkState.AddHandler(new PlayAnimationStateHandler(new AnimationPlayer(_animationsConfig.WalkAnimation, _animationRoot)));
            return walkState;
        }

        private State CreateIdleState()
        {
            var idleState = new State {DebugName = "Idle state"};
            idleState.AddHandler(new PlayAnimationStateHandler(new AnimationPlayer(_animationsConfig.IdleAnimation, _animationRoot)));
            return idleState;
        }
    }
}