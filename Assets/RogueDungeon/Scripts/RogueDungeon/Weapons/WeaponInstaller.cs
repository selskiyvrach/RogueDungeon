using Common.GameObjectMarkers;
using Common.ZenjectUtils;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private WeaponConfig _config;
        [SerializeField] private WeaponAnimationRootObject _animationRoot;

        public override void InstallBindings()
        {
            Container.InstanceSingleInterfaces(_config);
            Container.InstanceSingle(_animationRoot);
            Container.InstanceSingle<IAttackMediator>(new DummyAttackMediator());
            Container.InstanceSingle<IAttackInputProvider>(new DummyAttackInputProvider(() => Input.GetMouseButtonDown(0)));
            
            Container.NewSingleNonLazy<WeaponAnimator>();
            Container.NewSingleNonLazy<WeaponBehaviour>();
        }
    }
}