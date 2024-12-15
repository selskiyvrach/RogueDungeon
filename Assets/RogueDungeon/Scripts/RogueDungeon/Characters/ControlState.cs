namespace RogueDungeon.Characters
{
    public class ControlState : IControlState
    {
        public bool IsDodging { get; set; }
        public bool IsAttackInSoftPhase { get; set; }
        public bool IsAttackInHardPhase { get; set; }

        public bool CanDodge() => 
            !IsAttackInHardPhase;

        public bool CanAttack() => 
            !IsDodging;
    }
}