using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public ItemConfig DefaultWeapon { get; private set; }
        [field: SerializeField] public PlayerInstaller Prefab { get; private set; }
    }
}