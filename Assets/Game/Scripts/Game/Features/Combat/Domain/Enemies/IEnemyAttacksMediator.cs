namespace Game.Features.Combat.Domain.Enemies
{
    public interface IEnemyAttacksMediator
    {
        void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection);
    }
}