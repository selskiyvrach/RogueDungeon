using Common.Animations;
using Common.MoveSets;
using Common.UtilsUnity;
using Common.UtilsZenject;
using Inventory.Presenter;
using Inventory.View;
using Player.Model;
using Player.Model.Behaviours.Common;
using Player.Model.Behaviours.Hands;
using UnityEngine;
using Zenject;

namespace Player.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private TransformAnimationTarget _bodyAnimationTarget;
        [SerializeField] private TransformAnimationTarget _handsAnimationTarget;

        public override void InstallBindings()
        {
            InstallInventory();
            
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingleInterfacesAndSelf<Model.Player>();

            Container.NewSingle<PlayerControlStateMediator>();
            Container.NewSingleInterfaces<ItemWielderFacade>();
            
            _handsInstaller.Install(Container);
            
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");
            Container.InstanceSingle(new MoveSetFactory(Container).Create(_config.MoveSetConfig));
            Container.Bind<PlayerBehaviour>().AsSingle();

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<PlayerBehaviour>();
            Container.Resolve<Model.Player>().SetBehaviours(hands, movement);
        }

        private void InstallInventory()
        {
            Container.NewSingleInterfacesAndSelf<InventoryPresenter>();
            Container.NewSingle<Inventory.Model.Inventory>();
            Container.NewSingle<WorldInventoryItemFactory>();
            Container.InstanceSingle(_inventoryView);
            Container.Resolve<InventoryPresenter>().Construct(Container.Resolve<Inventory.Model.Inventory>(), Container.Resolve<InventoryView>());
        }
    }
}