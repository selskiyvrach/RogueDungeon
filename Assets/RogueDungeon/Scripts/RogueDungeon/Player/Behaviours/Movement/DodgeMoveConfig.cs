using System;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class DodgeMoveConfig : PlayerMoveConfig
    {
        [field: SerializeField] public PlayerDodgeState DodgeState { get; private set; }

        public override Type MoveType => typeof(DodgeMove);
    }
}