using RogueDungeon.Player.Commands;
using RogueDungeon.Player.States;
using RogueDungeon.StateMachine;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private CommandsReader _commandsReader;
        [SerializeField] private WalkAnimationPlayer _walkAnimation;
        [SerializeField] private IdleAnimationPlayer _idleAnimation;
        [SerializeField] private DodgeRightAnimationPlayer _dodgeRightAnimation;
        [SerializeField] private DodgeLeftAnimationPlayer _dodgeLeftAnimation;
        [SerializeField] private TestComboCreator _testComboCreator;
        
        public override void InstallBindings()
        {
            var idleState = new IdleState();
            idleState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IIdleAnimation>(_idleAnimation));
            
            var walkState = new WalkState();
            walkState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IWalkAnimation>(_walkAnimation));
            walkState.AddAllHandlerInterfaces(new WalkAnimationKeyframesSoundHandler(_walkAnimation));

            var dodgeRightState = new DodgeRightState(_dodgeRightAnimation);
            dodgeRightState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IDodgeAnimation>(_dodgeRightAnimation));
            
            var dodgeLeftState = new DodgeLeftState(_dodgeLeftAnimation);
            dodgeLeftState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IDodgeAnimation>(_dodgeLeftAnimation));
        
            var hasWalkInputCondition = new HasInputCondition(_commandsReader, Command.MoveForward);
            var doesNotHaveWalkInputCondition = new Negator(hasWalkInputCondition);
            var hasDodgeRightInputCondition = new HasInputCondition(_commandsReader, Command.DodgeRight);
            var hasDodgeLeftInputCondition = new HasInputCondition(_commandsReader, Command.DodgeLeft);
            var hasAttackInputCondition = new HasInputCondition(_commandsReader, Command.Attack);

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

            _testComboCreator.Construct(_commandsReader, _commandsReader);
            var attackState = new AttackComboState(_testComboCreator);
            stateMachineBuilder.AddState(attackState);
            
            stateMachineBuilder.AddTransitionFromToState<IdleState, AttackComboState>(hasAttackInputCondition);
            stateMachineBuilder.AddTransitionFromFinishedState<AttackComboState, IdleState>();

            var player = new Player(stateMachineBuilder.Build());
            Container.Bind<Player>().To<Player>().FromInstance(player);
            player.Run();
        }
    }
}