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
    
        public override void InstallBindings()
        {
            var idleState = new IdleState();
            idleState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IIdleAnimation>(_idleAnimation));
            
            var walkState = new WalkState();
            walkState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IWalkAnimation>(_walkAnimation));
        
            var hasWalkInputCondition = new HasWalkInputCondition();
            var doesNotHaveWalkInputCondition = new Negator(hasWalkInputCondition);

            var stateMachineBuilder = new StateMachineBuilder();
            stateMachineBuilder.AddState(walkState);
            stateMachineBuilder.AddState(idleState);
            stateMachineBuilder.SetStartState(idleState);
        
            stateMachineBuilder.AddTransitionFromToState<IdleState, WalkState>(hasWalkInputCondition);
            stateMachineBuilder.AddTransitionFromToState<WalkState, IdleState>(doesNotHaveWalkInputCondition);

            var player = new Player(stateMachineBuilder.Build());
            Container.Bind<Player>().To<Player>().FromInstance(player);
            player.Run();
        }
    }
}