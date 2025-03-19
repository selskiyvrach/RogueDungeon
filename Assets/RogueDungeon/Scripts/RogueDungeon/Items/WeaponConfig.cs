using UnityEngine;

namespace RogueDungeon.Items
{
    public class WeaponConfig : ItemConfig
    {
        [field: SerializeField] public float Damage { get; private set; } = 10;
        [field: SerializeField] public float PoiseDamage { get; private set; } = 10;
        [field: SerializeField] public float AttackStaminaCost { get; private set; } = 10;
    }
}