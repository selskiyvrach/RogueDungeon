using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay
{
    public class Player : Character, IEventHandler<DodgeEvent>, IEventHandler<AttackEvent>
    {
        public AvailableInteractions AvailableInteractions { get; }
        public PlayerDodgeState DodgeState { get; private set; }
        public EnemyPosition CurrentAttackDirection => EnemyPosition.Middle;

        public Player(IEventBus<IAnimationEvent> animationEvents, AvailableInteractions availableInteractions, StateMachine stateMachine) : base(animationEvents, stateMachine)
        {
            AvailableInteractions = availableInteractions;
            AnimationEvents.AddHandler<DodgeEvent>(this);
            AnimationEvents.AddHandler<AttackEvent>(this);
            Enable();
        }
 
        public void Handle(DodgeEvent @event) => 
            DodgeState = @event.ToDodgeState();

        public void Handle(AttackEvent @event)
        {
            var target = EntitiesRegistry.GetEnemyAtPosition(CurrentAttackDirection);
            target?.TakeDamage(AttackDamage);
        }
    }
}