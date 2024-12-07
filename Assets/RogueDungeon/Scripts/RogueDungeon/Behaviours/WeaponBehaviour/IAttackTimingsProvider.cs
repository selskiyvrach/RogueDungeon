namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public interface IAttackTimingsProvider
    {
        float GetPrepareDuration();
        float GetExecuteDuration();
        float GetFinishDuration();
    }
}