using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerInstaller Prefab { get; private set; }
    }
}