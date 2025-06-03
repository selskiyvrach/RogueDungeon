using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Animations
{
    public abstract class TransformAnimationsConfig : ScriptableObject
    {
        [field: HideLabel, BoxGroup(nameof(DefaultIdlePosition)), SerializeField] public KeyFrame DefaultIdlePosition { get; private set; }
        
        private void OnValidate()
        {
            var animationConfigs = new List<TransformAnimationConfig>();
            var type = GetType();

            while (type != null && type != typeof(MonoBehaviour))
            {
                var fields = type
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(f => typeof(TransformAnimationConfig).IsAssignableFrom(f.FieldType))
                    .Select(f => f.GetValue(this))
                    .OfType<TransformAnimationConfig>();

                animationConfigs.AddRange(fields);
                type = type.BaseType;
            }

            foreach (var config in animationConfigs)
            {
                for (var i = 0; i < config.KeyFrames.Length; i++)
                {
                    var frame = config.KeyFrames[i];
                    if(frame.KeyframeType == KeyFrame.Type.DefaultIdlePosition)
                        config.KeyFrames[i] = new KeyFrame(frame.Time, DefaultIdlePosition.Position, DefaultIdlePosition.Rotation, frame.KeyframeType);
                }
            }
        }
    }
}