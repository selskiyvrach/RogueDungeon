using System;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerRotationConfig : PlayerMoveConfig
    {
        [field: SerializeField] public float RotationDegrees { get; private set; }
        public override Type MoveType => typeof(PlayerRotationMove);
    }
}