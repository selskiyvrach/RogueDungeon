using System;

namespace RogueDungeon.Scripts.RogueDungeon.AttackHandling
{
    public interface IAttackHitEventObservable
    {
        event Action<IAttackingSource> OnHit;
    }
}