using Common.DotNetUtils;
using Common.Events;
using Common.FSM;
using Common.Registries;
using Common.UnityUtils;
using RogueDungeon.Animations;
using RogueDungeon.Camera;
using RogueDungeon.Entities;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;

namespace RogueDungeon.Player
{
    public class Player : IGameEntity
    {
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private readonly StateMachine _behaviour;
        private readonly IGameCamera _gameCamera;
        private readonly IRootObject<UnityEngine.Camera> _cameraRoot;

        public IRegistry<Parameter> Parameters { get; } = new Registry<Parameter>();
        public IRegistry<Property> Properties { get; } = new Registry<Property>();

        public Player(IEventBus<IAnimationEvent> animationEvents, StateMachine behaviour, IGameCamera gameCamera, IRootObject<UnityEngine.Camera> cameraRoot)
        {
            _behaviour = behaviour;
            _gameCamera = gameCamera;
            _cameraRoot = cameraRoot;
            _gameCamera.Follow = _cameraRoot.Transform;
            _gameCamera.Follow = cameraRoot.Transform;
            // Properties.Add(new Property<DodgeState>(DodgeState.None));
            // Properties.Add(new Property<BlockState>(new BlockState(null)));
            //
            // _animationEvents = animationEvents;
            // _animationEvents.AddHandler(new BlockEventHandler(Properties.Get<Property<BlockState>>()));
            // _animationEvents.AddHandler(new DodgeEventHandler(Properties.Get<Property<DodgeState>>()));
            // _animationEvents.AddHandler(new AttackHandler());
        }

    }
}