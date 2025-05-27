using System;
using Common.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemies.States
{
    [Serializable]
    public class EnemyAttackMoveConfig
    {
        [field: HorizontalGroup("1"), SerializeField] public string Name { get; private set; }
        [field: HorizontalGroup("1"), SerializeField] public EnemyPosition SuitableForPositions { get; private set; } = EnemyPosition.None;
        [field: HorizontalGroup, SerializeField] public float Duration { get; private set; }
        [field: HorizontalGroup, SerializeField] public float Damage { get; private set; }
        [field: HorizontalGroup, SerializeField] public EnemyAttackDirection Direction { get; private set; }
        [field: HideLabel, BoxGroup(nameof(Animation)), SerializeField] public SpriteSheetAnimationConfig Animation { get; private set; }
    }
}