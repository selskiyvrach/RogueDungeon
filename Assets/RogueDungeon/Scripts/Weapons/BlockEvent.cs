using RogueDungeon.Animations;

namespace RogueDungeon.Weapons
{
    public readonly struct BlockEvent : IAnimationEvent
    {
        public readonly IWeapon BlockingWeapon;

        public BlockEvent(IWeapon blockingWeapon) => 
            BlockingWeapon = blockingWeapon;
    }
}