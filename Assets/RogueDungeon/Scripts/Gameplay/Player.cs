using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay
{
    public class Player : Character
    {
        public Equipment Equipment { get; }
        public PlayerDodgeState DodgeState { get; private set; }
        public EnemyPosition CurrentAttackDirection => EnemyPosition.Middle;

        public Player(IEventBus<IAnimationEvent> animationEvents, Equipment equipment, StateMachine stateMachine) : base(animationEvents, stateMachine)
        {
            Equipment = equipment;
            AnimationEvents.Subscribe<DodgeEvent>(@event => DodgeState = @event.ToDodgeState());
            Enable();
        }

        protected override IDamageable GetAttackTarget() => 
            EntitiesRegistry.GetEnemyAtPosition(CurrentAttackDirection);
    }
}