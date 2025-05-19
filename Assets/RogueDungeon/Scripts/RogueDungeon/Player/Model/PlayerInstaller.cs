using Common.Animations;
using Common.MoveSets;
using Common.Unity;
using Common.UtilsZenject;
using RogueDungeon.Player.Model.Behaviours.Common;
using RogueDungeon.Player.Model.Behaviours.Hands;
using RogueDungeon.Player.Model.Inventory;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private InventoryView _inventoryPrefab;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private TransformAnimationTarget _bodyAnimationTarget;
        [SerializeField] private TransformAnimationTarget _handsAnimationTarget;

        public override void InstallBindings()
        {
            Container.NewSingle<Inventory.Inventory>();
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.InstantiatePrefab(_inventoryPrefab, transform);
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingle<Player>();

            Container.NewSingle<PlayerControlStateMediator>();
            
            _handsInstaller.Install(Container);
            
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");
            Container.InstanceSingle(new MoveSetFactory(Container).Create(_config.MoveSetConfig));
            Container.Bind<PlayerBehaviour>().AsSingle();

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<PlayerBehaviour>();
            Container.Resolve<Player>().SetBehaviours(hands, movement);
        }
    }
}