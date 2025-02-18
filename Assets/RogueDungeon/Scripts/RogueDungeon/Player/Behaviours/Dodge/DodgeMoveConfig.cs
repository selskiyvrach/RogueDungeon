using System;
using RogueDungeon.Combat;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeMoveConfig : PlayerMoveConfig
    {
        [field: SerializeField] public PlayerDodgeState DodgeState { get; private set; }

        public override Type MoveType => typeof(DodgeMove);
    }
}