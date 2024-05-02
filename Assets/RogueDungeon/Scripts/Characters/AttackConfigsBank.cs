using RogueDungeon.Data;
using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/AttackConfigsBank", fileName = "AttackConfigsBank", order = 0)]
    public class AttackConfigsBank : ScriptableDictionary<AttackConfig>
    {
        
    }
}