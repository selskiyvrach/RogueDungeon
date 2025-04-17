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
        public KeyFrame(float time, Vector3 position, Vector3 rotation)
        {
            Time = time;
            Position = position;
            Rotation = rotation;
        }

        [field: Range(0, 1), SerializeField] public float Time { get; set; }
        [field: SerializeField] public Vector3 Position { get; set; }
        [field: SerializeField] public Vector3 Rotation { get; set; }
    }
}