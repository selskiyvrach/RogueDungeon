using RogueDungeon.Animations;
using RogueDungeon.Characters;
using RogueDungeon.Enemies;
using RogueDungeon.Gameplay;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Player
{
    public class Player : Character, IEventHandler<DodgeEvent>, IEventHandler<AttackEvent>
    {
        private readonly EquipmentManager _equipmentManager;
        private readonly EnemyPosition _currentAttackDirection = EnemyPosition.Middle;
        private readonly AvailableInteractions _availableInteractions;
        public PlayerDodgeState DodgeState { get; private set; }

        public Player(IEventBus<IAnimationEvent> animationEvents, StateMachine stateMachine, EquipmentManager equipmentManager, AvailableInteractions availableInteractions) : base(animationEvents, stateMachine)
        {
            _equipmentManager = equipmentManager;
            _availableInteractions = availableInteractions;
            AnimationEvents.AddHandler<DodgeEvent>(this);
            AnimationEvents.AddHandler<AttackEvent>(this);
            Enable();
        }
 
        public void Handle(DodgeEvent @event) => 
            DodgeState = @event.ToDodgeState();

        public void Handle(AttackEvent @event)
        {
            var target = EntitiesRegistry.GetEnemyAtPosition(_currentAttackDirection);
            target?.TakeDamage(AttackDamage);
        }
    }
}