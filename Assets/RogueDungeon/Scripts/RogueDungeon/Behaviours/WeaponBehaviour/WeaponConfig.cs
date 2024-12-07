using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public class WeaponConfig : ScriptableObject, IAttackComboConfig
    {
        [field: SerializeField] public AttackConfig[] Attacks { get; private set; }
        
        public int Count => Attacks.Length;
        
        public IAttackTimingsProvider GetTimings(int attackIndex) => 
            Attacks[attackIndex];
    }
}