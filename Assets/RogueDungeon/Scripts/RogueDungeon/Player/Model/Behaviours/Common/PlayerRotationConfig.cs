using System;
using UnityEngine;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class PlayerRotationConfig : PlayerMoveConfig
    {
        [field: SerializeField] public float RotationDegrees { get; private set; }
        public override Type MoveType => typeof(TurnMove);
    }
}