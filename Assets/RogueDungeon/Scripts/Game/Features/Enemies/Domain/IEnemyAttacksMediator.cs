namespace Game.Features.Enemies.Domain
{
    public interface IEnemyAttacksMediator
    {
        void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection);
    }
}