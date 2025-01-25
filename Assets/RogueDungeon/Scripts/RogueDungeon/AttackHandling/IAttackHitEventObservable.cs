using System;

namespace RogueDungeon.AttackHandling
{
    public interface IAttackHitEventObservable
    {
        event Action<IAttackingSource> OnHit;
    }
}