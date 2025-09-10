namespace Game.Libs.Combat
{
    public readonly struct PlayerAttackResult
    {
        public readonly PlayerAttackInfo AttackInfo;
        public readonly bool IsHit;
        public readonly float FinalDamage;
        public readonly float FinalPoiseDamage;

        public PlayerAttackResult(bool isHit, float finalDamage, float finalPoiseDamage, PlayerAttackInfo attackInfo)
        {
            IsHit = isHit;
            FinalDamage = finalDamage;
            FinalPoiseDamage = finalPoiseDamage;
            AttackInfo = attackInfo;
        }
    }
}