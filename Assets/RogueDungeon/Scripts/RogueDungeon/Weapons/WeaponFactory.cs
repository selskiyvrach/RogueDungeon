using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace RogueDungeon.Weapons
{
    public class WeaponFactory : IFactory<WeaponConfig, Transform, IWeapon>
    {
        private readonly DiContainer _container;

        public WeaponFactory(DiContainer container) => 
            _container = container;

        public IWeapon Create(WeaponConfig config, Transform parent)
        {
            var instance = Object.Instantiate(config.Prefab, parent, false);
            var installer = instance.GetComponent<IWeaponInstaller>();
            if (installer == null)
                throw new InvalidOperationException("Prefab must contain a component implementing IWeaponInstaller.");

            installer.InstallBindings(config, _container);
            return installer.ResolveWeapon();
        }
    }
}