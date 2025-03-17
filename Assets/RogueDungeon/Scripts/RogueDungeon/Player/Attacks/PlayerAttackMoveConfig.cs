using System;
using Common.MoveSets;
using RogueDungeon.Input;
using RogueDungeon.Player.Behaviours;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMoveConfig : PlayerAttackBaseMoveConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        public override Type MoveType => typeof(PlayerAttackMove);
    }
}