using System;
using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class PlayerMoveConfig : MoveConfig
    {
        [field: SerializeField] public Input.InputKey RequiredInputKey { get; private set; }
        public override Type MoveType => typeof(PlayerMove);
    }
}