using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Animations
{
    public class Animator : MonoBehaviour
    {
        [SerializeField] private GameObject _animationRoot;
        [SerializeField] private AnimationClip[] _animations;
        [CanBeNull] private AnimationClip _current;
        
        public void SetState([CanBeNull] string state)
        {
            if (state == null)
            {
                _current = null;
                return;
            }
            _current = _animations.FirstOrDefault(n => n.name == state);
            Assert.IsNotNull(_current, $"Animation '{state}' not found");
        }

        public void UpdateState(float durationNormalized)
        {
            if(_current != null)
                _current.SampleAnimation(_animationRoot, _current.length * durationNormalized);
        }
    }
}