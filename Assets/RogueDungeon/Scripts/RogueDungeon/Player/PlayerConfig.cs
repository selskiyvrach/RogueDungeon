using RogueDungeon.Items;
using RogueDungeon.Player.Stamina;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Player
{
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; } = 100f;
        [field: SerializeField, InlineProperty] public StaminaConfig StaminaConfig { get; private set; }
        [field: SerializeField, Range(0.01f, 0.49f)] public float PositionOffsetFromTileCenter { get; private set; } = .35f;
        [field: SerializeField] public WeaponConfig DefaultWeapon { get; private set; }
        [field: SerializeField] public PlayerInstaller Prefab { get; private set; }
    }
}