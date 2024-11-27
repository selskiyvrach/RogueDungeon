using System;
using Common.Events;
using Common.FSM;
using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.Camera;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;

namespace RogueDungeon.Player
{
    public class Player : IGameEntity, IDodger
    {
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private readonly IRootObject<UnityEngine.Camera> _cameraRoot;
        private readonly StateMachine _behaviour;
        private readonly IGameCamera _gameCamera;
        private readonly DodgeHandler _dodgeHandler;

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
        }
    }
}