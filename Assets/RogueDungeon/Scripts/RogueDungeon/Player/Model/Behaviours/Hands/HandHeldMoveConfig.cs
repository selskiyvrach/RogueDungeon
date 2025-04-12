using System;
using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class HandHeldMoveConfig : MoveConfig
    {
        [field: SerializeField] public float Duration { get; private set; } = .5f;

        public override Type MoveType => Id switch {
            "idle" => typeof(HandHeldIdle),
            "unsheath" => typeof(UnsheathMove),
            "sheath" => typeof(SheathMove),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}