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
        private readonly ICharacterInput _characterInput;
        private readonly PlayerAnimationsConfig _animationsConfig;
        private readonly IRootObject<AnimationPlayer> _animationRoot;
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private IAvailableInteractionsProvider _availableInteractions;

        public PlayerBehaviourStateMachineFactory(
            ICharacterInput characterInput, 
            PlayerAnimationsConfig animationsConfig, 
            IRootObject<AnimationPlayer> animationRoot, 
            IEventBus<IAnimationEvent> animationEvents) 
        {
            _characterInput = characterInput;
            _animationsConfig = animationsConfig;
            _animationRoot = animationRoot;
            _animationEvents = animationEvents;
        }

        public StateMachine Create()
        {
            var idleState = CreateIdleState();
            var walkState = CreateWalkState();
            var dodgeRightState = CreateDodgeRightState(out var dodgeRightAnimation);
            var dodgeLeftState = CreateDodgeLeftState(out var dodgeLeftAnimation);
            var interactionState = new InteractionState(_availableInteractions);
        
            var hasWalkInputCondition = new HasInputCondition(_characterInput, Command.MoveForward);
            var doesNotHaveWalkInputCondition = new Not(hasWalkInputCondition);
            var hasDodgeRightInputCondition = new HasInputCondition(_characterInput, Command.DodgeRight);
            var hasDodgeLeftInputCondition = new HasInputCondition(_characterInput, Command.DodgeLeft);

            var builder = new StateMachineBuilder();
            builder.AddState(walkState);
            builder.AddState(idleState);
            builder.AddState(dodgeRightState);
            builder.AddState(dodgeLeftState);
            builder.SetStartState(idleState);
        
            builder.AddTransition(idleState, walkState, hasWalkInputCondition);
            builder.AddTransition(walkState, idleState, doesNotHaveWalkInputCondition);
            
            builder.AddTransition(idleState, dodgeRightState, hasDodgeRightInputCondition);
            builder.AddTransitionFromFinished(dodgeRightState, idleState, new AnimationToFinishable(dodgeRightAnimation));
            
            builder.AddTransition(idleState, dodgeLeftState, hasDodgeLeftInputCondition);
            builder.AddTransitionFromFinished(dodgeLeftState, idleState, new AnimationToFinishable(dodgeLeftAnimation));
            
            builder.SetDebugName("Player root state machine");
            
            builder.AddState(interactionState);
            
            builder.AddTransition(idleState, interactionState, interactionState);
            builder.AddTransition(idleState, interactionState, interactionState);
            builder.AddTransitionFromFinished(interactionState, idleState, interactionState);

            return builder.Build();
        }

        private State CreateDodgeLeftState(out AnimationPlayer dodgeLeftAnimation)
        {
            var dodgeLeftState = new State {DebugName = "Dodge left state"};
            dodgeLeftAnimation = new AnimationPlayer(_animationsConfig.DodgeLeftAnimation, _animationRoot);
            dodgeLeftState.AddHandler(new PlayAnimationStateHandler(dodgeLeftAnimation));
            dodgeLeftState.AddHandler(new ConsumeCommandStateEnterHandler(_characterInput, Command.DodgeLeft));
            
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
            dodgeRightState.AddHandler(new ConsumeCommandStateEnterHandler(_characterInput, Command.DodgeRight));
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