using UnityEngine;

namespace Enemies.HiveMind
{
    public class HiveMindConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 1)] public float BattleHeatFactorWhenTheresAnAttackGoing { get; private set; } = .5f;
        [field: SerializeField, Range(0, 1)] public float BattleHeatFactorPerExtraEnemy { get; private set; } = .75f;
    }
}