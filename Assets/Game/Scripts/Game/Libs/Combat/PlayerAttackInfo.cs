namespace Game.Libs.Combat
{
    public readonly struct PlayerAttackInfo
    {
        public readonly float Damage;
        public readonly float PoiseDamage;
        public readonly EnemyPosition TargetPosition;

        public PlayerAttackInfo(float damage, float poiseDamage, EnemyPosition targetPosition)
        {
            Damage = damage;
            TargetPosition = targetPosition;
            PoiseDamage = poiseDamage;
        }
    }
}