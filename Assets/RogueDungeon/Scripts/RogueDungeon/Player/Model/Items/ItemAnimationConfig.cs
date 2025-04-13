using System;
using Common.Animations;
using UnityEngine;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace RogueDungeon.Items
{
    [Serializable]
    public class ItemAnimationConfig : AnimationConfig
    {
        [field: SerializeField] public Vector3 EndPosition { get; private set; }
        [field: SerializeField] public Vector3 EndRotation { get; private set; }
        [field: SerializeField] public AnimationEvent[] Events { get; private set; }
        public override Type AnimationType => typeof(ItemAnimation);
    }
}