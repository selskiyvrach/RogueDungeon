using UnityEngine;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveFromPositionToPositionDuration { get; private set; } = 0.5f;
    }
}