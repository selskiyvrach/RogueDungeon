namespace RogueDungeon.Enemies
{
    public interface IEnemyAttacksMediator
    {
        void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection);
    }
}