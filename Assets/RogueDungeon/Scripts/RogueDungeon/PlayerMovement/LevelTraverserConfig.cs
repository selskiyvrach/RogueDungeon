using UnityEngine;

namespace PlayerMovement
{
    public class LevelTraverserConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveDuration { get; private set; } = 1;
        [field: SerializeField] public float RotationDuration { get; private set; } = 1;
    }
}