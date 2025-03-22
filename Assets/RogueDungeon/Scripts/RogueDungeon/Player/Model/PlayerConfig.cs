using RogueDungeon.Characters;
using RogueDungeon.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Player.Model
{
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, InlineProperty] public ResourceConfig Health { get; private set; }
        [field: SerializeField, InlineProperty] public RechargeableResourceConfig Stamina { get; private set; }
        [field: SerializeField, Range(0.01f, 0.49f)] public float PositionOffsetFromTileCenter { get; private set; } = .35f;
        [field: SerializeField] public WeaponConfig DefaultWeapon { get; private set; }
        [field: SerializeField] public PlayerInstaller Prefab { get; private set; }
    }
}