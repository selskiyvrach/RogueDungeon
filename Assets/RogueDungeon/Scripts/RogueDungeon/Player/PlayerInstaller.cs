using Common.Animations;
using Common.Parameters;
using Common.UtilsZenject;
using RogueDungeon.Characters;
using RogueDungeon.Items.Weapons;
using RogueDungeon.Parameters;
using RogueDungeon.PlayerInput;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private AnimationPlayer _equipmentAnimator;
        [SerializeField] private WeaponConfig _weaponCofig;
        [SerializeField] private ParametersPicker _parameterPicker;
        private Player _player;

        public override void InstallBindings()
        {
            // Container.Resolve<IGameCamera>().Follow = _cameraParent;
            Container.InstanceSingle<IParameters>(_parameterPicker.ToParameters());
            Container.NewSingle<IControlState, ControlState>();
            Container.NewSingle<IInput, CharacterInput>();
            Container.InstanceSingle(_weaponCofig);
            Container.InstanceSingle<IAnimator>(_equipmentAnimator);
            _player = Container.NewSingleResolve<Player>();
        }

        public override void Start()
        {
            base.Start();
            _player.Initialize();
        }
    }
}