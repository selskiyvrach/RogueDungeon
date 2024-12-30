using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponTimings : ScriptableObject
    {
        [field: SerializeField] public float IdleAnimationSpeed { get; private set; } = 1;
        [field: SerializeField] public float AttackExecutionDuration { get; private set; }= .3f;
        [field: SerializeField] public float AttackAttackTransitionDuration { get; private set; }= .4f;
        [field: SerializeField] public float AttackIdleTransitionDuration { get; private set; }= .4f;
        
    }
}