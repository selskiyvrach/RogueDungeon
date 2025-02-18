namespace RogueDungeon.Combat
{
    public interface IAttacksMediator
    {
        void MediatePlayerAttack(IPlayerAttackInfo attackInfo);
        void MediateEnemyAttack(IEnemyAttackInfo attackInfo);
    }
}