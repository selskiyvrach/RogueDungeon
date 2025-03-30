using UnityEngine;

namespace RogueDungeon.Combat
{
    public class CombatFeedbackConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 OnHitCameraPunch { get; private set; }
        [field: SerializeField] public float OnHitCameraPunchDuration { get; private set; }
    }
}