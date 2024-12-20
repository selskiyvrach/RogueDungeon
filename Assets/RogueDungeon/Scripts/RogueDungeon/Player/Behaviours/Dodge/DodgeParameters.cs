using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeParameters : ScriptableObject
    {
        [field: SerializeField] public float Duration { get; private set; }
    }
}