using Common.Unity;
using Common.UtilsZenject;
using RogueDungeon.Player.Behaviours.Hands;
using RogueDungeon.Player.Behaviours.Movement;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private PlayerMovementBehaviourInstaller _movementBehaviourInstaller;

        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.NewSingle<PlayerBlockerHandler>();
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingle<Player>();

            Container.NewSingle<PlayerControlStateMediator>();
            
            _handsInstaller.Install(Container);
            _movementBehaviourInstaller.Install(Container);

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<PlayerMovementBehaviour>();
            Container.Resolve<Player>().SetBehaviours(hands, movement);
        }
    }
}