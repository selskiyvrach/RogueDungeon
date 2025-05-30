using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Animations
{
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

        public override string ToString() => 
            $"Time: {Time:0.00}, Pos: {Position}, Rot: {Rotation}";
    }
}