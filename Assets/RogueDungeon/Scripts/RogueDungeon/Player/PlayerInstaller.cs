using Common.Parameters;
using Common.UtilsZenject;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Characters;
using RogueDungeon.Parameters;
using RogueDungeon.PlayerInput;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private ParametersPicker _parameterPicker;
        [SerializeField] private WeaponConfig _weaponCofig;
        private Player _player;

        public override void InstallBindings()
        {
            // Container.Resolve<IGameCamera>().Follow = _cameraParent;
            Container.InstanceSingle<IParameters>(_parameterPicker.ToParameters());
            Container.NewSingle<IControlState, ControlState>();
            Container.NewSingle<IInput, CharacterInput>();
            Container.InstanceSingle<WeaponConfig>(_weaponCofig);
            _player = Container.NewSingleResolve<Player>();
        }

        public override void Start()
        {
            base.Start();
            _player.Initialize();
        }
    }
}