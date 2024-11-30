using System;
using Common.Events;
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
        private readonly IGameCamera _gameCamera;
        private IDisposable _sub;
        public Positions Position => DodgeState.ToPlayerPosition();
        public DodgeState DodgeState { get; set; }

        public Player(IGameCamera gameCamera, IRootObject<UnityEngine.Camera> cameraRoot)
        {
            _gameCamera = gameCamera;
            _cameraRoot = cameraRoot;
            _gameCamera.Follow = _cameraRoot.Transform;
            _gameCamera.Follow = cameraRoot.Transform;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}