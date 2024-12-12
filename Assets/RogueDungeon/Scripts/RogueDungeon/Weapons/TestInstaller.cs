using Common.ZenjectUtils;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class TestInstaller : MonoInstaller
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private Transform _weaponParent;
        [SerializeField] private WeaponScreenSpaceAnimator _screenSpaceAnimator;
        [SerializeField] private WeaponActionsDurations _actionsDurations;
        
        public override void InstallBindings()
        {
            // environment
            Container.InstanceSingle(_screenSpaceAnimator);
            Container.InstanceSingle<IAttackActionsDurationsProvider, WeaponActionsDurations>(_actionsDurations);
            
            // item
            Container.InstanceSingleInterfaces(_weaponConfig);
            Container.NewSingle<IFactory<WeaponConfig, Transform, IWeapon>, WeaponFactory>();
            Container.Resolve<IFactory<WeaponConfig, Transform, IWeapon>>().Create(_weaponConfig, _weaponParent);
        }
    }
}