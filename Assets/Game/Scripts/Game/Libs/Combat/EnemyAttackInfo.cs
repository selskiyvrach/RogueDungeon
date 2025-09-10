namespace Game.Libs.Combat
{
    public readonly struct EnemyAttackInfo
    {
        public readonly float Damage;
        public readonly AttackDirection Direction;
        public readonly EnemyPosition Position;

        public EnemyAttackInfo(float damage, AttackDirection direction, EnemyPosition position)
        {
            Damage = damage;
            Direction = direction;
            Position = position;
        }
    }
}