using System;
using Common.Events;
using Common.FSM;
using Common.Providers;
using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.Camera;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using RogueDungeon.PlayerInputCommands;
using UniRx;

namespace RogueDungeon.Player
{
    public class PlayerRootStateMachineBuildingDirector
    {
        private IPlayerInput _input;
        
        public void Create()
        {
            var attack = new Value<bool>();
            var isAttacking = new ValueCondition(attack);
            
            
            var dodge = new Value<bool>();
            var isDodging = new ValueCondition(dodge);
            
            var dodgeRight = new TimerState(1).Bind(dodge);
            var dodgeLeft = new TimerState(1).Bind(dodge);
            var dodgeIdle = new State();

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(new Not(isAttacking), new HasCommand(Command.DodgeRight, _input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(new Not(isAttacking), new HasCommand(Command.DodgeLeft, _input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
        }
    }
    
    
    
    
    
    
    
    
    
    

    public class Player : IGameEntity, IDodger
    {
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private readonly IRootObject<UnityEngine.Camera> _cameraRoot;
        private readonly StateMachine _behaviour;
        private readonly IGameCamera _gameCamera;
        private readonly DodgeHandler _dodgeHandler;
        private IDisposable _sub;

        public Positions Position => _dodgeHandler.ToPlayerPosition();
        public DodgeState DodgeState => _dodgeHandler.DodgeState;

        public Player(IEventBus<IAnimationEvent> animationEvents, StateMachine behaviour, IGameCamera gameCamera, IRootObject<UnityEngine.Camera> cameraRoot)
        {
            _behaviour = behaviour;
            _gameCamera = gameCamera;
            _cameraRoot = cameraRoot;
            _gameCamera.Follow = _cameraRoot.Transform;
            _gameCamera.Follow = cameraRoot.Transform;
            animationEvents.AddHandler(_dodgeHandler = new DodgeHandler());
            Enable();
        }

        public void Disable()
        {
            _sub?.Dispose();
            _behaviour.Stop();
        }

        public void Enable()
        {
            _sub = Observable.EveryUpdate().Subscribe(_ => _behaviour.Tick());
            _behaviour.Run();
        }
    }
}