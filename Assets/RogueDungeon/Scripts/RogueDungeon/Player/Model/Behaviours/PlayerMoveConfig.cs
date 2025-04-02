using System;
using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Player.Model.Behaviours
{
    public class PlayerMoveConfig : MoveConfig
    {
        public override Type MoveType => typeof(PlayerMove);
    }
}