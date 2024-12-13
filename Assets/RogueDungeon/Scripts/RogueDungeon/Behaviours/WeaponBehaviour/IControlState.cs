namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IControlState
    {
        bool Is(AbleTo ableTo);
        bool IsInUncancellableAnimation { set; }
    }
}