using System;
using Common.Animations;
using UnityEngine;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace RogueDungeon.Items
{
    [Serializable]
    public class ItemAnimationConfig : AnimationConfig
    {
        [field: SerializeField] public KeyFrame[] KeyFrames { get; private set; }
        [field: SerializeField] public AnimationEvent[] Events { get; private set; }
        public override Type AnimationType => typeof(ItemAnimation);
    }

    [Serializable]
    public struct KeyFrame
    {
        [field: Range(0, 1), SerializeField] public float Time { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }
    }
}