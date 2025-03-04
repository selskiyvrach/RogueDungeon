using System;
using RogueDungeon.Input;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMoveConfig : AttackMoveConfig
    {
        [field: SerializeField] public InputKey RequiredInput { get; private set; }
        public override Type MoveType => typeof(PlayerAttackMove);
    }
}