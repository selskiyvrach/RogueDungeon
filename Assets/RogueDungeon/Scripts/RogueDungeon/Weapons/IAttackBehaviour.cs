using System;
using Common.Behaviours;
using Common.ScreenSpaceEffects;

namespace RogueDungeon.Weapons
{
    public interface IAttackBehaviour : IBehaviour
    {
        IAttackActionsDurationsProvider Durations { get; }
        ScreenSpaceDirection CurrentAttackDirection { get; }
        event Action OnPrepareAttackStarted;
        event Action OnExecuteAttackStarted;
        event Action OnHitKeyframe;
        event Action OnFinishAttackStarted;
    }
}