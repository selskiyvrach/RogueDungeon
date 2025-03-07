using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Animations
{
    [Serializable]
    public class SpriteSheetAnimationConfig : AnimationConfigWithDuration
    {
        [PreviewField(70, ObjectFieldAlignment.Center)]
        [field: SerializeField] public Sprite[] Sprites {get; private set;}
        [field: SerializeField] public FrameEvent[] KeyFrames {get; private set;}
        
        public override Type AnimationType => typeof(SpriteSheetAnimation);
    }
}