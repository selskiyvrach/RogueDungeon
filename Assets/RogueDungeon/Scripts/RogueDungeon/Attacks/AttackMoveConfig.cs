using Common.MoveSets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public abstract class AttackMoveConfig : MoveConfig
    {
        [field: SerializeField] public bool JustAnimation { get; private set; } = true;
        [field: HideIf(nameof(JustAnimation)), SerializeField] public float Damage { get; private set; }
    }
}