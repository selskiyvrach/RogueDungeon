using UnityEngine;

namespace RogueDungeon.Items
{
    public class BaseItemTimings : ScriptableObject
    {
        [field: SerializeField] public float IdleAnimationSpeed { get; private set; } = 1;
        [field: SerializeField] public float AttackExecutionDuration { get; private set; }= .3f;
        [field: SerializeField] public float AttackAttackTransitionDuration { get; private set; }= .4f;
        [field: SerializeField] public float AttackIdleTransitionDuration { get; private set; }= .4f;
        [field: SerializeField] public float UnsheathDuration { get; private set; } = .5f;
    }
}