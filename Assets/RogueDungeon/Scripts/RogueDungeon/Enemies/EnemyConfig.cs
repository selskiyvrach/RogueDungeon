using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyInstaller Prefab { get; private set; }
    }
}