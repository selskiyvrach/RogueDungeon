using System;
using JetBrains.Annotations;
using UnityEngine;

namespace RogueDungeon.Actions
{
    [Serializable]
    public class ActionConfig
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string AnimationName { get; private set; }
        [field: SerializeField] public string Command { get; private set; }
        [field: SerializeField] public int Frames { get; private set; }
        [field: SerializeField] public Keyframe[] Keyframes { get; private set; }

        [CanBeNull]
        public string GetKeyframe(int frame)
        {
            if (!(frame > 0 && frame <= Frames)) 
                Debug.Log("Keyframe is out of range");

            foreach (var keyframe in Keyframes)
            {
                if (keyframe.Frame == frame)
                    return keyframe.Name;
            }

            return null;
        }
    }
}