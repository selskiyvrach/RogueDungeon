namespace RogueDungeon.Combat
{
    public interface IEnemyAttackInfo : IDamageInfo
    {
        EnemyAttackDirection AttackDirection { get; }
    }
}