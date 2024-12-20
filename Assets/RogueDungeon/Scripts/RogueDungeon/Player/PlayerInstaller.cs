using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Input;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private AnimationPlayer _equipmentAnimator;
        [SerializeField] private WeaponConfig _weaponCofig;
        private Player _player;

        public override void InstallBindings()
        {
            Container.NewSingle<IControlState, ControlState>();
            Container.NewSingle<IInput, CharacterInput>();
            Container.InstanceSingle(_weaponCofig);
            Container.InstanceSingle<IAnimator>(_equipmentAnimator);
            Container.NewSingle<Player>();
        }

        public override void Start()
        {
            base.Start();
            _player = Container.Resolve<Player>();
            _player.Initialize();
        }
    }
}