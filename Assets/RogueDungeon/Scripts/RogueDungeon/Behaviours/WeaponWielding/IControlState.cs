namespace RogueDungeon.Behaviours.WeaponWielding
{
    public interface IControlState
    {
        bool Is(AbleTo ableTo);
        bool IsInUncancellableAnimation { set; }
    }
}