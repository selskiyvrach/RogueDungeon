using Game.Features.Player.App.Presenters;
using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.CommonMoveset;
using Game.Features.Player.Infrastructure.Configs;
using Game.Features.Player.Infrastructure.View;
using Libs.Animations;
using Libs.Fsm;
using Libs.Movesets;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Installers
{
    public class PlayerInstanceInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private TransformAnimationTarget _bodyAnimationTarget;
        [SerializeField] private TransformAnimationTarget _handsAnimationTarget;
        [SerializeField] private PlayerGameObjectPositionView _worldRootObject;

        public override void InstallBindings()
        {
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");

            Container.Bind<StateMachine>().FromMethod(() => new MoveSetFactory(Container).Create(Container.Resolve<PlayerConfig>().MoveSetConfig, new PlayerMoveIdToTypeConverter()));
            Container.BindInterfacesAndSelfTo<Domain.Player>().AsSingle();
            Container.Bind<PlayerControlStateMediator>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerGameObjectPositionView>().FromInstance(_worldRootObject).AsSingle();
            Container.Bind<PlayerGameObjectPositionPresenter>().AsSingle().NonLazy();
        }
    }
}