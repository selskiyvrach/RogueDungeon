using UnityEngine;

namespace Game.Libs.Feedback
{
    public class CombatFeedbackConfig : ScriptableObject
    {
        [field: SerializeField] public float OnHitCameraPunch { get; private set; }
        [field: SerializeField] public float OnHitCameraPunchDuration { get; private set; }
        [field: SerializeField] public float OnCriticalHitCameraPunch { get; private set; }
        [field: SerializeField] public float OnCriticalHitCameraShakeDuration { get; private set; }
    }
}