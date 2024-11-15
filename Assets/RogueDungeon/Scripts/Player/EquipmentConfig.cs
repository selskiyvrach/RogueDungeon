using RogueDungeon.Configs;
using RogueDungeon.Weapons;
using UnityEngine;

namespace RogueDungeon.Player
{
    [CreateAssetMenu(menuName = "Configs/Player/EquipmentConfig", fileName = "PlayerEquipmentConfig", order = 0)]
    public class EquipmentConfig : Config
    {
        [field: SerializeField] public WeaponConfig DefaultWeaponConfig { get; private set; }
    }
}