namespace RogueDungeon.Characters
{
    public interface IControlState
    {
        bool CanDodge();
        bool CanAttack();
        bool IsDodging { set; }
        bool IsAttackInSoftPhase { set; }
        bool IsAttackInHardPhase { set; }
    }
}