using UnityEngine;

namespace Common.UI.Bars
{
    public class BarDeltaConfig : ScriptableObject
    {
        [field: SerializeField] public float Delay { get; private set; } = .5f;
        [field: SerializeField] public float CatchDuration { get; private set; } = 1f;
    }
}