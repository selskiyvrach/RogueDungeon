using Common.DotNetUtils;
using Common.Events;
using Common.FSM;
using Common.Registries;
using RogueDungeon.Animations;
using RogueDungeon.Entities.Properties;
using RogueDungeon.Player;

namespace RogueDungeon.Entities
{
    public class Player : RootEntity
    {
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private readonly StateMachine _behaviour;

        public Player(IEventBus<IRootEvent> gameEvents, IRegistry<IRootEntity> gameEntities, IEventBus<IAnimationEvent> animationEvents, StateMachine behaviour) : base(gameEvents, gameEntities)
        {
            _behaviour = behaviour;
            Properties.Add(new Property<DodgeState>(DodgeState.None));
            Properties.Add(new Property<BlockState>(new BlockState(null)));
            
            _animationEvents = animationEvents;
            _animationEvents.AddHandler(new BlockEventHandler(Properties.Get<Property<BlockState>>()));
            _animationEvents.AddHandler(new DodgeEventHandler(Properties.Get<Property<DodgeState>>()));
            _animationEvents.AddHandler(new AttackHandler(GameEntities));
        }
    }
}