using UnityEngine;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindConfig : ScriptableObject
    {
        [field: SerializeField] public float SlackTime { get; private set; } = 1f;
    }
}