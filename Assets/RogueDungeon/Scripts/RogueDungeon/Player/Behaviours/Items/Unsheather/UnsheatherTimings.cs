using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class UnsheatherTimings : ScriptableObject
    {
        [field: SerializeField] public float UnsheathDuration { get; private set; } = .5f;
    }
}