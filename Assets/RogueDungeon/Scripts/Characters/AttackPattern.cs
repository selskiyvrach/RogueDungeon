using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/AttackPattern", fileName = "AttackPattern", order = 0)]
    public class AttackPattern : ScriptableObject
    {
        [field: SerializeField] public Positions SuitableForPositions { get; private set; }
        [field: SerializeField] public int ChillFrames { get; private set; }
        [field: SerializeField] public AttackConfig[] AttackConfigs { get; private set; }
    }
}