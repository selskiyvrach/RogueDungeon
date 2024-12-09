using UnityEngine;

namespace RogueDungeon.Weapons
{
    public interface IAttackAnimationsProvider
    {
        AnimationClip PrepareAnimation { get; }
        AnimationClip ExecuteAnimation { get; }
        AnimationClip FinishAnimation { get; }
    }
}