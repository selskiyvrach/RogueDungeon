using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class WeaponConfig : ScriptableObject, IWeaponInfo
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public float Weight { get; private set;}
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public DamageType Type { get; private set; }
        [field: SerializeField] public AttackDirection[] AttackDirectionsInCombo { get; private set; } = {
            AttackDirection.BottomLeft, AttackDirection.BottomRight
        };
    }
}