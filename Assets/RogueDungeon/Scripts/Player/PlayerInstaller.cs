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
        [SerializeField] private WeaponManipulatorStateMachineCreator _weaponManipulatorStateMachineCreator;
        
        public override void InstallBindings()
        {
            var idleState = new State();
            idleState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IIdleAnimation>(_idleAnimation));
            
            var walkState = new State();
            walkState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IWalkAnimation>(_walkAnimation));
            walkState.AddAllHandlerInterfaces(new WalkAnimationKeyframesSoundHandler(_walkAnimation));

            var dodgeRightState = new FinishableState(_dodgeRightAnimation);
            dodgeRightState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IDodgeAnimation>(_dodgeRightAnimation));
            dodgeRightState.AddStateEnterHandler(new ConsumeCommandStateEnterHandler(_commandsReader, Command.DodgeRight));
            
            var dodgeLeftState = new FinishableState(_dodgeLeftAnimation);
            dodgeLeftState.AddAllHandlerInterfaces(new PlayAnimationStateHandler<IDodgeAnimation>(_dodgeLeftAnimation));
            dodgeLeftState.AddStateEnterHandler(new ConsumeCommandStateEnterHandler(_commandsReader, Command.DodgeLeft));
        
            var hasWalkInputCondition = new HasInputCondition(_commandsReader, Command.MoveForward);
            var doesNotHaveWalkInputCondition = new ConditionNegator(hasWalkInputCondition);
            var hasDodgeRightInputCondition = new HasInputCondition(_commandsReader, Command.DodgeRight);
            var hasDodgeLeftInputCondition = new HasInputCondition(_commandsReader, Command.DodgeLeft);
            var hasAttackInputCondition = new HasInputCondition(_commandsReader, Command.Attack);
            var hasBlockInputCondition = new HasInputCondition(_commandsReader, Command.Block);

            var stateMachineBuilder = new StateMachineBuilder();
            stateMachineBuilder.AddState(walkState);
            stateMachineBuilder.AddState(idleState);
            stateMachineBuilder.AddState(dodgeRightState);
            stateMachineBuilder.AddState(dodgeLeftState);
            stateMachineBuilder.SetStartState(idleState);
        
            stateMachineBuilder.AddTransitionFromToState(idleState, walkState, hasWalkInputCondition);
            stateMachineBuilder.AddTransitionFromToState(walkState, idleState, doesNotHaveWalkInputCondition);
            
            stateMachineBuilder.AddTransitionFromToState(idleState, dodgeRightState, hasDodgeRightInputCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(dodgeRightState, idleState);
            
            stateMachineBuilder.AddTransitionFromToState(idleState, dodgeLeftState, hasDodgeLeftInputCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(dodgeLeftState, idleState);

            _weaponManipulatorStateMachineCreator.Construct(_commandsReader, _commandsReader);
            var attackState = new EquipmentManipulationState(_weaponManipulatorStateMachineCreator);
            stateMachineBuilder.AddState(attackState);
            
            stateMachineBuilder.AddTransitionFromToState(idleState, attackState, hasAttackInputCondition);
            stateMachineBuilder.AddTransitionFromToState(idleState, attackState, hasBlockInputCondition);
            stateMachineBuilder.AddTransitionFromFinishedState(attackState, idleState);
            
            stateMachineBuilder.SetDebugName("Player root state machine");

            var player = new Player(stateMachineBuilder.Build());
            Container.Bind<Player>().To<Player>().FromInstance(player);
            player.Run();
        }
    }
}