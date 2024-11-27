using Common.Events;
using Common.FSM;
using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.PlayerInputCommands;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerBehaviourStateMachineFactory : IFactory<StateMachine>
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerAnimationsConfig _animationsConfig;
        private readonly IRootObject<AnimationPlayer> _animationRoot;
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private readonly AvailableInteractions _availableInteractions;

        public PlayerBehaviourStateMachineFactory(
            IPlayerInput playerInput, 
            PlayerAnimationsConfig animationsConfig, 
            IRootObject<AnimationPlayer> animationRoot, 
            IEventBus<IAnimationEvent> animationEvents, 
            AvailableInteractions availableInteractions)
        {
            _playerInput = playerInput;
            _animationsConfig = animationsConfig;
            _animationRoot = animationRoot;
            _animationEvents = animationEvents;
            _availableInteractions = availableInteractions;
        }

        public StateMachine Create()
        {
            var idleState = CreateIdleState();
            var walkState = CreateWalkState();
            var dodgeRightState = CreateDodgeRightState(out var dodgeRightAnimation);
            var dodgeLeftState = CreateDodgeLeftState(out var dodgeLeftAnimation);
            var interactionState = new InteractionState(_availableInteractions);
        
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
            
            builder.AddState(interactionState);
            
            builder.AddTransition(idleState, interactionState, interactionState);
            builder.AddTransition(idleState, interactionState, interactionState);
            builder.AddTransitionWhenFinished(interactionState, idleState, interactionState);

            return builder.Build();
        }

        private State CreateDodgeLeftState(out AnimationPlayer dodgeLeftAnimation)
        {
            var dodgeLeftState = new State {DebugName = "Dodge left state"};
            dodgeLeftAnimation = new AnimationPlayer(_animationsConfig.DodgeLeftAnimation, _animationRoot);
            dodgeLeftState.AddHandler(new PlayAnimationStateHandler(dodgeLeftAnimation));
            dodgeLeftState.AddHandler(new ConsumeCommandStateEnterHandler(_playerInput, Command.DodgeLeft));
            
            dodgeLeftState.AddHandler(new AnimationEventStateHandler<DodgeEvent>(_animationEvents, dodgeLeftAnimation, AnimEventNames.DODGE_STARTED, 
                new DodgeEvent(DodgeState.Left)));
            dodgeLeftState.AddHandler(new AnimationEventStateHandler<DodgeEvent>(_animationEvents, dodgeLeftAnimation, AnimEventNames.DODGE_ENDED, 
                new DodgeEvent(DodgeState.None)));
            return dodgeLeftState;
        }

        private State CreateDodgeRightState(out AnimationPlayer dodgeRightAnimation)
        {
            var dodgeRightState = new State {DebugName = "Dodge right state"};
            dodgeRightAnimation = new AnimationPlayer(_animationsConfig.DodgeRightAnimation, _animationRoot);
            dodgeRightState.AddHandler(new PlayAnimationStateHandler(dodgeRightAnimation));
            dodgeRightState.AddHandler(new ConsumeCommandStateEnterHandler(_playerInput, Command.DodgeRight));
            dodgeRightState.AddHandler(new AnimationEventStateHandler<DodgeEvent>(_animationEvents, dodgeRightAnimation, AnimEventNames.DODGE_STARTED, 
                new DodgeEvent(DodgeState.Right)));
            dodgeRightState.AddHandler(new AnimationEventStateHandler<DodgeEvent>(_animationEvents, dodgeRightAnimation, AnimEventNames.DODGE_ENDED, 
                new DodgeEvent(DodgeState.None)));
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