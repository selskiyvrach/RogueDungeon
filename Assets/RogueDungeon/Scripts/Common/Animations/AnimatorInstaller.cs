using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace Common.Animations
{
    public class AnimatorInstaller : MonoInstaller
    {
        [SerializeField] private Animator _animator;
        
        public override void InstallBindings() => 
            Container.InstanceSingle<IAnimator>(_animator);
    }
}