using Common.GameObjectMarkers;
using Common.ZenjectUtils;
using RogueDungeon.PlayerInput;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class WeaponInstaller : MonoBehaviour, IWeaponInstaller
    {
        [SerializeField] private WeaponAnimationRootObject _animationRoot;
        [SerializeField] private WeaponWorldSpaceAnimator _animator;

        private DiContainer _weaponContainer;

        public IWeapon ResolveWeapon() => _weaponContainer.Resolve<IWeapon>();

        public void InstallBindings(WeaponConfig config, DiContainer container)
        {
            _weaponContainer = container.CreateSubContainer();
            
            _weaponContainer.InstanceSingleInterfaces(config);
            _weaponContainer.InstanceSingle(_animationRoot);
            
            _weaponContainer.InstanceSingle<IAttackMediator>(new DummyAttackMediator());
            var input = new CharacterInput();
            _weaponContainer.InstanceSingle<IAttackInputProvider>(new DummyWeaponInputProvider(() => input.HasCommand(Command.Attack)));
            _weaponContainer.InstanceSingle<IAttackDamageModifier>(new DummyAttackDamageModifier());
            _weaponContainer.InstanceSingle(_animator);
            _weaponContainer.NewSingle<IWeaponAnimator, AttackAnimatorFacade>();
            _weaponContainer.NewSingle<AttackAnimatorController>();

            _weaponContainer.NewSingle<IAttackBehaviour, AttackBehaviour>();
            _weaponContainer.NewSingle<AttackHitHandler>();
            _weaponContainer.NewSingle<IWeapon, Weapon>();
        }
    }
}