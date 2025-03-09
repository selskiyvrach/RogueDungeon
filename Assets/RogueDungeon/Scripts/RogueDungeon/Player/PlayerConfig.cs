using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Range(0.01f, 0.49f)] public float PositionOffsetFromTileCenter { get; private set; } = .35f;
        [field: SerializeField] public ItemConfig DefaultWeapon { get; private set; }
        [field: SerializeField] public PlayerInstaller Prefab { get; private set; }
    }
}