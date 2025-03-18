using System;
using Common.MoveSets;
using RogueDungeon.Input;
using RogueDungeon.Player.Behaviours;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class PlayerAttackMoveConfig : PlayerAttackBaseMoveConfig
    {
        public override Type MoveType => typeof(PlayerAttackMove);
    }
}