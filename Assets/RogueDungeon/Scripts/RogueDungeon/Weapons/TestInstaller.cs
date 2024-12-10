using Common.Registries;
using Common.ZenjectUtils;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class TestInstaller : MonoInstaller
    {
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private Transform _weaponParent;
        
        public override void InstallBindings()
        {
            // environment
            Container.NewSingle<IRegistry<IGameEntity>, Registry<IGameEntity>>();
            Container.NewSingle<ICollisionsDetector, CollisionsDetector>();
            
            // item
            Container.InstanceSingleInterfaces(_weaponConfig);
            Container.NewSingle<IFactory<WeaponConfig, Transform, IWeapon>, WeaponFactory>();
            Container.Resolve<IFactory<WeaponConfig, Transform, IWeapon>>().Create(_weaponConfig, _weaponParent);
        }
    }
}