namespace Game.Libs.Combat
{
    public readonly struct EnemyAttackResult
    {
        public readonly bool IsHit;
        public readonly bool IsDodge;
        public readonly bool IsRightDodge;
        public readonly bool IsBlock;

        public readonly float FinalStaminaDamage;
        public readonly float FinalDamage;

        public static EnemyAttackResult NoResult => new();
        public static EnemyAttackResult DodgedLeft => new(isHit: false, isDodge: true, isRightDodge: false, isBlock: false, finalStaminaDamage: 0, finalDamage: 0); 
        public static EnemyAttackResult DodgedRight => new(isHit: false, isDodge: true, isRightDodge: true, isBlock: false, finalStaminaDamage: 0, finalDamage: 0);
        
        public static EnemyAttackResult NonBlockedHit(float healthDamage) => 
            new(isHit: true, isDodge: false, isRightDodge: false, isBlock: false, finalStaminaDamage: 0, finalDamage: healthDamage);
        
        public static EnemyAttackResult BlockedHit(float staminaDamage, float healthDamage) => 
            new(isHit: true, isDodge: false, isRightDodge: false, isBlock: true, finalStaminaDamage: staminaDamage, finalDamage: healthDamage);
        public EnemyAttackResult(bool isHit, bool isDodge, bool isRightDodge, bool isBlock, float finalStaminaDamage, float finalDamage)
        {
            IsHit = isHit;
            IsDodge = isDodge;
            IsRightDodge = isRightDodge;
            IsBlock = isBlock;
            FinalStaminaDamage = finalStaminaDamage;
            FinalDamage = finalDamage;
        }
    }
}