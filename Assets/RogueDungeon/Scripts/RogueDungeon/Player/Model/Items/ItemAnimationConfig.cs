using System;
using Common.Animations;
using Sirenix.OdinInspector;
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
        public enum Type
        {
            Custom,
            DefaultIdlePosition,
        }

        public KeyFrame(float time, Vector3 position, Vector3 rotation, Type keyframeType = Type.Custom)
        {
            Time = time;
            Position = position;
            Rotation = rotation;
            KeyframeType = keyframeType;
        }

        [field: SerializeField] public Type KeyframeType { get; private set; }
        [field: Range(0, 1), SerializeField] public float Time { get; private set; }
        [field: HideIf("@KeyframeType == Type.DefaultIdlePosition"), SerializeField] public Vector3 Position { get; private set; }
        [field: HideIf("@KeyframeType == Type.DefaultIdlePosition"), SerializeField] public Vector3 Rotation { get; private set; }
    }
}