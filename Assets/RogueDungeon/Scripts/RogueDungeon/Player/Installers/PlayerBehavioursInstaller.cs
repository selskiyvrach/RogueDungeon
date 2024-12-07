using Common.ZenjectUtils;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Behaviours.WeaponBehaviour;
using RogueDungeon.Player.Behaviours;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Installers
{
    public class PlayerBehavioursInstaller
    {
        public void Install(DiContainer container)
        {
            container.NewSingle<PlayerBehavioursMediator>();
            container.NewSingle<MovementBehaviour>();
        }
    }

    public class WeaponInstaller : ScriptableObject
    {
        [SerializeField] private WeaponAnimationsConfigsProvider _weaponAnimations;

        public void Install(DiContainer container)
        {
            var subContainer = container.CreateSubContainer();
            container.InstanceSingle<IWeaponAnimationsConfigsProvider, WeaponAnimationsConfigsProvider>(_weaponAnimations);
            container.NewSingle<WeaponBehaviour>();
        }
    }
}