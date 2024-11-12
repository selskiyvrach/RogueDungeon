using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay
{
    public class Player : Character
    {
        public Equipment Equipment { get; }
        public PlayerDodgeState DodgeState { get; }
        public bool IsBlockUp { get; }
        public EnemyPosition CurrentAttackDirection { get; }

        public Player(IEventBus<IAnimationEvent> animationEvents, Equipment equipment, StateMachine stateMachine) : base(animationEvents, stateMachine)
        {
            Equipment = equipment;
            Enable();
        }

        protected override IDamageable GetAttackTarget() => 
            EntitiesRegistry.GetEnemyAtPosition(CurrentAttackDirection);
    }
}