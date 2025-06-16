namespace Game.Libs.Combat
{
    public readonly struct DefenderInfo
    {
        public readonly bool IsAlive;
        public readonly AttackDirection DodgingAgainst;
        public readonly bool IsBlocking;
        public readonly float BlockingAbsorbtion;
        public readonly float BlockingStaminaCostFactor;

        public DefenderInfo(bool isAlive, AttackDirection dodgingAgainst, bool isBlocking,
            float blockingAbsorbtion, float blockingStaminaCostFactor)
        {
            IsAlive = isAlive;
            DodgingAgainst = dodgingAgainst;
            IsBlocking = isBlocking;
            BlockingAbsorbtion = blockingAbsorbtion;
            BlockingStaminaCostFactor = blockingStaminaCostFactor;
        }
    }
}