namespace Game.Libs.Combat
{
    public readonly struct EnemyAttackInfo
    {
        public readonly float Damage;
        public readonly AttackDirection Direction;

        public EnemyAttackInfo(float damage, AttackDirection direction)
        {
            Damage = damage;
            Direction = direction;
        }
    }
}