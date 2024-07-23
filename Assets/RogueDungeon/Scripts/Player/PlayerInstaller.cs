using RogueDungeon.Player.Commands;
using RogueDungeon.Player.States;
using RogueDungeon.StateMachine;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private WalkAnimationPlayer _walkAnimation;
        [SerializeField] private IdleAnimationPlayer _idleAnimation;
        [SerializeField] private DodgeRightAnimationPlayer _dodgeRightAnimation;
        [SerializeField] private DodgeLeftAnimationPlayer _dodgeLeftAnimation;
    
        public override void InstallBindings()
        {
            var commandsReader = new CommandsReader();
            
            var idleState = new IdleState();
            idleState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IIdleAnimation>(_idleAnimation));
            
            var walkState = new WalkState();
            walkState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IWalkAnimation>(_walkAnimation));
            walkState.AddAllHandlerInterfaces(new WalkAnimationKeyframesSoundHandler(_walkAnimation));

            var dodgeRightState = new DodgeRightState(_dodgeRightAnimation);
            dodgeRightState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IDodgeAnimation>(_dodgeRightAnimation));
            
            var dodgeLeftState = new DodgeLeftState(_dodgeLeftAnimation);
            dodgeLeftState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IDodgeAnimation>(_dodgeLeftAnimation));
        
            var hasWalkInputCondition = new HasInputCondition(commandsReader, Command.MoveForward);
            var doesNotHaveWalkInputCondition = new Negator(hasWalkInputCondition);
            var hasDodgeRightInputCondition = new HasInputCondition(commandsReader, Command.DodgeRight);
            var hasDodgeLeftInputCondition = new HasInputCondition(commandsReader, Command.DodgeLeft);

            var stateMachineBuilder = new StateMachineBuilder();
            stateMachineBuilder.AddState(walkState);
            stateMachineBuilder.AddState(idleState);
            stateMachineBuilder.AddState(dodgeRightState);
            stateMachineBuilder.AddState(dodgeLeftState);
            stateMachineBuilder.SetStartState(idleState);
        
            stateMachineBuilder.AddTransitionFromToState<IdleState, WalkState>(hasWalkInputCondition);
            stateMachineBuilder.AddTransitionFromToState<WalkState, IdleState>(doesNotHaveWalkInputCondition);
            
            stateMachineBuilder.AddTransitionFromToState<IdleState, DodgeRightState>(hasDodgeRightInputCondition);
            stateMachineBuilder.AddTransitionFromFinishedState<DodgeRightState, IdleState>();
            
            stateMachineBuilder.AddTransitionFromToState<IdleState, DodgeLeftState>(hasDodgeLeftInputCondition);
            stateMachineBuilder.AddTransitionFromFinishedState<DodgeLeftState, IdleState>();

            var player = new Player(stateMachineBuilder.Build());
            Container.Bind<Player>().To<Player>().FromInstance(player);
            player.Run();
        }
    }
}