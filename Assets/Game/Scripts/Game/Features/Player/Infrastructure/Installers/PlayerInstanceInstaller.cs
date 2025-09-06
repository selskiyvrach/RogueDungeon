using Game.Features.Player.App.Presenters;
using Game.Features.Player.App.UseCases.Instance;
using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.CommonMoveset;
using Game.Features.Player.Domain.Behaviours.Hands;
using Game.Features.Player.Domain.Movesets.Items;
using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Features.Player.Infrastructure.Configs;
using Game.Features.Player.Infrastructure.View;
using Game.Libs.InGameResources;
using Libs.Animations;
using Libs.Fsm;
using Libs.Movesets;
using Libs.UI.Bars;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Installers
{
    public class PlayerInstanceInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Transform _cameraParent;
        
        [SerializeField] private TransformAnimationTarget _bodyAnimationTarget;
        [SerializeField] private TransformAnimationTarget _handsAnimationTarget;
        [SerializeField] private PlayerGameObjectPositionView _worldRootObject;
        
        [SerializeField] private TransformAnimationTarget _leftHandAnimationTarget; 
        [SerializeField] private TransformAnimationTarget _rightHandAnimationTarget;
        [SerializeField] private HandheldItemView _leftItemView;
        [SerializeField] private HandheldItemView _rightItemView;
        
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Bar _staminaBar;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");

            Container.Bind<StateMachine>().FromMethod(() => new MoveSetFactory(Container).Create(Container.Resolve<PlayerConfig>().MoveSetConfig, new PlayerMoveIdToTypeConverter()));
            Container.BindInterfacesAndSelfTo<Domain.Player>().AsSingle();
            Container.Bind<PlayerControlStateMediator>().AsSingle();
            
            Container.BindInterfacesTo<PlayerToItemMovesetFacade>().AsSingle();
            BindHand(isRightHand: true);
            BindHand(isRightHand: false);
            Container.Bind<PlayerHandsBehaviour>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerGameObjectPositionView>().FromInstance(_worldRootObject).AsSingle();
            Container.Bind<PlayerGameObjectPositionPresenter>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<SyncLevelAndLevelContextUseCase>().AsSingle().NonLazy();
            Container.Bind<EquipDefaultWeaponsUseCase>().AsSingle().NonLazy();
            Container.Bind<SyncDrawnWeaponsWithInventoryStateUseCase>().AsSingle().NonLazy();
            Container.Bind<MediateAttacksUseCase>().AsSingle().NonLazy();
            Container.Bind<SetPlayerReferenceToAttacksMediatorUseCase>().AsSingle().NonLazy();
            
            Container.Bind<ResourceBarPresenter>().FromMethod(ctx =>
                Container.Instantiate<ResourceBarPresenter>(new object[] {ctx.Container.Resolve<Domain.Player>().Health, _healthBar})).AsCached().NonLazy();

            Container.Bind<ResourceBarPresenter>().FromMethod(ctx =>
                Container.Instantiate<ResourceBarPresenter>(new object[] {ctx.Container.Resolve<Domain.Player>().Stamina, _staminaBar})).AsCached().NonLazy();
            
            Container.BindInterfacesTo<PlayerInstanceInstaller>().FromInstance(this).AsCached().NonLazy();
        }

        private void BindHand(bool isRightHand)
        {
            Container.Bind<HandBehaviour>().WithId(
                    isRightHand ? 
                        PlayerHandsBehaviour.RIGHT_HAND_INJECTION_ID : 
                        PlayerHandsBehaviour.LEFT_HAND_INJECTION_ID)
                .FromSubContainerResolve().ByMethod(CreateHandSubContainer).AsCached();
            return;

            void CreateHandSubContainer(DiContainer handContainer)
            {
                handContainer.Bind<TransformAnimationTarget>().FromInstance(isRightHand ? _rightHandAnimationTarget : _leftHandAnimationTarget).AsSingle();
                handContainer.Bind<IHandheldItemView>().FromInstance(isRightHand ? _rightItemView : _leftItemView).AsSingle();
                handContainer.Bind<IMoveIdToTypeConverter>().To<ItemMoveIdToTypeConverter>().AsSingle();
                handContainer.Bind<MoveSetFactory>().AsSingle();
                handContainer.Bind<ItemMovesetFactory>().AsSingle();
                handContainer.Bind<HandBehaviour>().AsSingle().WithArguments(new object[]{isRightHand});
                handContainer.Bind<IItemSwapper>().To<HandToItemSwapperAdapter>().AsSingle();
                handContainer.Bind<HandPresenter>().AsSingle().NonLazy();
            }
        }

        public void Initialize() => 
            Container.Resolve<Camera>().transform.SetParent(_cameraParent, worldPositionStays: false);
    }
}