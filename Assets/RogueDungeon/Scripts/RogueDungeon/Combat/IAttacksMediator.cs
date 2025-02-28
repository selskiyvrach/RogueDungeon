namespace RogueDungeon.Combat
{
    public interface IAttacksMediator
    {
        void MediatePlayerAttack(float damage);
        void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection);
    }
}