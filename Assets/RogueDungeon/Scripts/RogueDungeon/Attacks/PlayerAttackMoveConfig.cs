using System;
using RogueDungeon.Combat;
using RogueDungeon.Input;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMoveConfig : AttackMoveConfig, IPlayerAttackInfo
    {
        [field: SerializeField] public InputKey RequiredInput { get; private set; }
        public override Type MoveType => typeof(PlayerAttackMove);
    }
}