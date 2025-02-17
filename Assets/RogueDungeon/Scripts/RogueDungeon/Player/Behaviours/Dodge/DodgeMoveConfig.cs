using System;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeMoveConfig : PlayerMoveConfig
    {
        [field: SerializeField] public DodgeState DodgeState { get; private set; }

        public override Type MoveType => typeof(DodgeMove);
    }
}