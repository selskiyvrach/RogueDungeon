using UnityEngine;

namespace Common.MoveSets
{
    public class MoveSetConfig : ScriptableObject, IMoveSetConfig
    {
        [field: SerializeField] public AnimationClip IdleAnimation { get; private set; }
        [field: SerializeField] public float IdleAnimationSpeed { get; private set; } = 1;
        [field: SerializeField] public MoveConfig[] MoveConfigs { get; private set; }
    }
}