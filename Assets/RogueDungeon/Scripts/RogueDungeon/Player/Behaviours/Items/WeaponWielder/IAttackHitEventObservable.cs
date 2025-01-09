using System;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public interface IAttackHitEventObservable
    {
        event Action OnHit;
    }
}