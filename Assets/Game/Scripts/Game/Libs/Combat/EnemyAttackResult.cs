namespace Game.Libs.Combat
{
    public readonly struct EnemyAttackResult
    {
        public readonly EnemyAttackInfo AttackInfo;
        public readonly bool IsHit;
        public readonly bool IsDodge;
        public readonly bool IsRightDodge;
        public readonly bool IsBlock;

        public readonly float FinalStaminaDamage;
        public readonly float FinalDamage;

        public static EnemyAttackResult NoResult(EnemyAttackInfo info) => new();
        public static EnemyAttackResult DodgedLeft(EnemyAttackInfo attackInfo) => 
            new(isHit: false, isDodge: true, isRightDodge: false, isBlock: false, finalStaminaDamage: 0, finalDamage: 0, attackInfo); 
        public static EnemyAttackResult DodgedRight(EnemyAttackInfo attackInfo) => 
            new(isHit: false, isDodge: true, isRightDodge: true, isBlock: false, finalStaminaDamage: 0, finalDamage: 0, attackInfo);
        public static EnemyAttackResult NonBlockedHit(float healthDamage, EnemyAttackInfo attackInfo) => 
            new(isHit: true, isDodge: false, isRightDodge: false, isBlock: false, finalStaminaDamage: 0, finalDamage: healthDamage, attackInfo);
        
        public static EnemyAttackResult BlockedHit(float staminaDamage, float healthDamage, EnemyAttackInfo attackInfo) => 
            new(isHit: true, isDodge: false, isRightDodge: false, isBlock: true, finalStaminaDamage: staminaDamage, finalDamage: healthDamage, attackInfo);
        public EnemyAttackResult(bool isHit, bool isDodge, bool isRightDodge, bool isBlock, float finalStaminaDamage, float finalDamage, EnemyAttackInfo attackInfo)
        {
            IsHit = isHit;
            IsDodge = isDodge;
            IsRightDodge = isRightDodge;
            IsBlock = isBlock;
            FinalStaminaDamage = finalStaminaDamage;
            FinalDamage = finalDamage;
            AttackInfo = attackInfo;
        }
    }
}