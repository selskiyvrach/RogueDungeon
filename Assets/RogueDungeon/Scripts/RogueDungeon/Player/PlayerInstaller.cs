using Common.UtilsZenject;
using RogueDungeon.Input;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;

        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerGameObject);
            Container.NewSingle<IPlayerInput, PlayerInput>();
            Container.NewSingleInterfacesAndSelf<Player>();
        }
    }
}