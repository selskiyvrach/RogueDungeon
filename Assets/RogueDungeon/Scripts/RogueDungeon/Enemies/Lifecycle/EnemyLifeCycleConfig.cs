using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyLifeCycleConfig : ScriptableObject
    {
        [field: SerializeField] public AnimationClip BirthAnimation { get; private set; }
        [field: SerializeField] public float BirthDuration { get; private set; }
        [field: SerializeField] public AnimationClip DeathAnimation { get; private set; }
        [field: SerializeField] public float DeathDuration { get; private set; }
        [field: SerializeField] public AnimationClip BeingAliveAnimation { get; private set; }
    }
}