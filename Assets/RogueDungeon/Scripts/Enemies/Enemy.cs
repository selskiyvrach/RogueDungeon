using RogueDungeon.Animations;
using RogueDungeon.Characters;
using RogueDungeon.Player;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Enemies
{
    public class Enemy : Character
    {
        public EnemyPosition Position { get; }
        public EnemyAttackType CurrentAttackDirection { get; }

        public Enemy(IEventBus<IAnimationEvent> animationEvents, StateMachine stateMachine) : base(animationEvents, stateMachine)
        {
        }

        protected IDamageable GetAttackTarget()
        {
            var player = EntitiesRegistry.Player; 
            return player.DodgeState switch
            {
                PlayerDodgeState.Left when CurrentAttackDirection == EnemyAttackType.Right => null,
                PlayerDodgeState.Right when CurrentAttackDirection == EnemyAttackType.Left => null,
                _ => player,
            };
        }
    }
}