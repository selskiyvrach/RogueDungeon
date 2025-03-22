using Common.Unity;
using Common.UtilsZenject;
using RogueDungeon.Player.Model.Behaviours.Common;
using RogueDungeon.Player.Model.Behaviours.Hands;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private PlayerCommonBehaviourInstaller _commonBehaviourInstaller;

        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingle<Player>();

            Container.NewSingle<PlayerControlStateMediator>();
            
            _handsInstaller.Install(Container);
            _commonBehaviourInstaller.Install(Container);

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<PlayerCommonBehaviour>();
            Container.Resolve<Player>().SetBehaviours(hands, movement);
        }
    }
}