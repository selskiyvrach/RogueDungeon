namespace RogueDungeon.Weapons
{
    public interface IAttackTimingsProvider
    {
        float GetPrepareDuration();
        float GetExecuteDuration();
        float GetFinishDuration();
    }
}