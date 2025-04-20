using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Common.Animations
{
    [Serializable]
    public class SpriteSheetAnimationConfig : AnimationConfig
    {
        [PreviewField(70, ObjectFieldAlignment.Center)]
        [field: SerializeField, ListDrawerSettings(ShowFoldout = false)] public Sprite[] Sprites {get; private set;}
        [field: SerializeField, ListDrawerSettings(ShowFoldout = false)] public FrameEvent[] KeyFrames {get; private set;}
        
        public override IAnimation Create(DiContainer container) => 
            (SpriteSheetAnimation)container.Instantiate(typeof(SpriteSheetAnimation), new object[]{ this});
    }
}