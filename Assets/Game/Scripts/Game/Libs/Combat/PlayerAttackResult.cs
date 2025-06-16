namespace Game.Libs.Combat
{
    public readonly struct PlayerAttackResult
    {
        public readonly bool IsHit;
        public readonly float FinalDamage;
        public readonly float FinalPoiseDamage;

        public PlayerAttackResult(bool isHit, float finalDamage, float finalPoiseDamage)
        {
            IsHit = isHit;
            FinalDamage = finalDamage;
            FinalPoiseDamage = finalPoiseDamage;
        }
    }
}