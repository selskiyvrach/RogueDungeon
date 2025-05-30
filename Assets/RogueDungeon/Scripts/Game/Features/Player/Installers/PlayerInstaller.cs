using Game.Features.Inventory.Presenter;
using Game.Features.Inventory.View;
using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.Common;
using Game.Features.Player.Domain.Behaviours.Hands;
using Libs.Animations;
using Libs.Movesets;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Installers
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
            Container.NewSingleInterfacesAndSelf<PlayerModel>();

            Container.NewSingle<PlayerControlStateMediator>();
            Container.NewSingleInterfaces<ItemWielderFacade>();
            
            _handsInstaller.Install(Container);
            
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");
            Container.InstanceSingle(new MoveSetFactory(Container).Create(_config.MoveSetConfig));
            Container.Bind<PlayerBehaviour>().AsSingle();

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<PlayerBehaviour>();
            var player = Container.Resolve<PlayerModel>(); 
            player.SetBehaviours(hands, movement);
            _handsInstaller.Initialize();
        }

        private void InstallInventory()
        {
            Container.NewSingleInterfacesAndSelf<InventoryPresenter>();
            Container.NewSingle<Inventory.Domain.Inventory>();
            Container.NewSingle<WorldInventoryItemFactory>();
            Container.InstanceSingle(_inventoryView);
            Container.Resolve<InventoryPresenter>().Construct(Container.Resolve<Inventory.Domain.Inventory>(), Container.Resolve<InventoryView>());
        }
    }
}