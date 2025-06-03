using System;
using UnityEngine;
using Zenject;

namespace Libs.Animations
{
    [Serializable]
    public class AnimationClipAdapterConfig : AnimationConfig
    {
        [field: SerializeField] public AnimationClip Clip { get; private set; }
        public override IAnimation Create(DiContainer container) => 
            (AnimationClipAdapter)container.Instantiate(typeof(AnimationClipAdapter), new object[]{this});
    }
}