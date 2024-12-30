using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Characters.Commands;
using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Input;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private AnimationPlayer _animationPlayer;
        [SerializeField] private WeaponConfig _weaponCofig;
        private Player _player;

        public override void InstallBindings()
        {
            Container.NewSingle<IControlState, ControlState>();
            Container.NewSingle<ICharacterCommands, PlayerInput>();
            Container.InstanceSingle(_weaponCofig);
            Container.InstanceSingle<IAnimator>(_animationPlayer);
            Container.NewSingleInterfacesAndSelf<Player>();
            Container.AutoResolve<Player>();
        }
    }
}