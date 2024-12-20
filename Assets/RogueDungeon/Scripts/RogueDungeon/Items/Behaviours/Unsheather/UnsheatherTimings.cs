using UnityEngine;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class UnsheatherTimings : ScriptableObject
    {
        [field: SerializeField] public float UnsheathDuration { get; private set; } = .5f;
    }
}