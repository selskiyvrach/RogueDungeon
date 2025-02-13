using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Characters.Input;
using RogueDungeon.Items.Data.Weapons;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private DodgeAnimationsPlayer _animationPlayer;
        [SerializeField] private WeaponConfig _weaponCofig;
        private Player _player;

        public override void InstallBindings()
        {
            Container.NewSingleInterfaces<PlayerControlStateMediator>();
            Container.AutoResolve<PlayerControlStateMediator>();
            
            Container.NewSingle<ICharacterInput, PlayerInput>();
            Container.InstanceSingle(_weaponCofig);
            Container.InstanceSingle<IAnimator>(_animationPlayer);
            Container.NewSingleInterfacesAndSelf<Player>();
            Container.AutoResolve<Player>();
        }
    }
}