using Common.GameObjectMarkers;
using Common.ZenjectUtils;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class WeaponInstaller : MonoBehaviour, IWeaponInstaller
    {
        [SerializeField] private WeaponAnimationRootObject _animationRoot;

        private DiContainer _weaponContainer;

        public IWeapon ResolveWeapon() => _weaponContainer.Resolve<IWeapon>();

        public void InstallBindings(WeaponConfig config, DiContainer container)
        {
            _weaponContainer = container.CreateSubContainer();
            
            _weaponContainer.InstanceSingleInterfaces(config);
            _weaponContainer.InstanceSingle(_animationRoot);
            
            _weaponContainer.InstanceSingle<IAttackMediator>(new DummyAttackMediator());
            _weaponContainer.InstanceSingle<IAttackInputProvider>(new DummyAttackInputProvider(() => Input.GetMouseButtonDown(0)));
            _weaponContainer.InstanceSingle<IAttackDamageModifier>(new DummyAttackDamageModifier());

            _weaponContainer.NewSingle<WeaponAnimator>();
            _weaponContainer.NewSingle<WeaponBehaviour>();
            _weaponContainer.NewSingle<AttackHitHandler>();
            _weaponContainer.NewSingle<IWeapon, Weapon>();
        }
    }
}