namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public struct PlayOptions
    {
        public readonly Animation Type;
        public readonly float Duration;

        public PlayOptions(Animation type, float duration)
        {
            Type = type;
            Duration = duration;
        }
    }
}