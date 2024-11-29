using System;
using Common.Events;
using Common.FSM;
using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.Camera;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using UniRx;

namespace RogueDungeon.Player
{
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