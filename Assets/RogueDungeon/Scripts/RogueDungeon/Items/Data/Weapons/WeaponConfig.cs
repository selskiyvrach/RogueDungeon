using RogueDungeon.MoveSets;
using UnityEngine;

namespace RogueDungeon.Items.Data.Weapons
{
    public class WeaponConfig : ScriptableObject, IWeaponInfo
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public float Weight { get; private set;}
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public DamageType DamageType { get; private set; }
        [field: SerializeField] public MoveSetConfig MoveSetConfig { get; private set; }
    }
}