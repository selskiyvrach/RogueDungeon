using RogueDungeon.Animations;
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
        [SerializeField] private AnimationPlayer _walkAnimation;
        [SerializeField] private AnimationPlayer _idleAnimation;
        [SerializeField] private AnimationPlayer _dodgeRightAnimation;
        [SerializeField] private AnimationPlayer _dodgeLeftAnimation;
        [SerializeField] private WeaponManipulatorStateMachineCreator _weaponManipulatorStateMachineCreator;
        
        public override void InstallBindings()
        {
            var idleState = new State {DebugName = "Idle state"};
            idleState.AddHandler(new PlayAnimationStateHandler(_idleAnimation));
            
            var walkState = new State {DebugName = "Walk state"};
            walkState.AddHandler(new PlayAnimationStateHandler(_walkAnimation));
            walkState.AddHandler(new PlayAnimationStateHandler(_walkAnimation));

            var dodgeRightState = new State {DebugName = "Dodge right state"};
            dodgeRightState.AddHandler(new PlayAnimationStateHandler(_dodgeRightAnimation));
            dodgeRightState.AddHandler(new ConsumeCommandStateEnterHandler(_commandsReader, Command.DodgeRight));
            
            var dodgeLeftState = new State {DebugName = "Dodge left state"};
            dodgeLeftState.AddHandler(new PlayAnimationStateHandler(_dodgeLeftAnimation));
            dodgeLeftState.AddHandler(new ConsumeCommandStateEnterHandler(_commandsReader, Command.DodgeLeft));
        
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
            stateMachineBuilder.AddTransitionWhenFinished(dodgeRightState, idleState, new AnimationPlayerToFinishableAdapter(_dodgeRightAnimation));
            
            stateMachineBuilder.AddTransitionFromToState(idleState, dodgeLeftState, hasDodgeLeftInputCondition);
            stateMachineBuilder.AddTransitionWhenFinished(dodgeLeftState, idleState, new AnimationPlayerToFinishableAdapter(_dodgeLeftAnimation));

            _weaponManipulatorStateMachineCreator.Construct(_commandsReader, _commandsReader);
            var attackState = new EquipmentManipulationState(_weaponManipulatorStateMachineCreator);
            stateMachineBuilder.AddState(attackState);
            
            stateMachineBuilder.AddTransitionFromToState(idleState, attackState, hasAttackInputCondition);
            stateMachineBuilder.AddTransitionFromToState(idleState, attackState, hasBlockInputCondition);
            stateMachineBuilder.AddTransitionWhenFinished(attackState, idleState, attackState);
            
            stateMachineBuilder.SetDebugName("Player root state machine");

            var player = new Player(stateMachineBuilder.Build());
            Container.Bind<Player>().To<Player>().FromInstance(player);
            player.Run();
        }
    }
}