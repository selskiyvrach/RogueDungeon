using Common.Unity;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private PlayerConfig _config;

        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingleInterfacesAndSelf<Player>();
        }
    }
}