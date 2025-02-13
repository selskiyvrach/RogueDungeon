using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace Common.Animations
{
    public class AnimationPlayerInstaller : MonoInstaller
    {
        [SerializeField] private AnimationPlayer _animator;
        
        public override void InstallBindings() => 
            Container.InstanceSingle<IAnimator>(_animator);
    }
}