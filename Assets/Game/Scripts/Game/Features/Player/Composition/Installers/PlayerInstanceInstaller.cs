using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.CommonMoveset;
using Game.Features.Player.Infrastructure.Configs;
using Game.Libs.WorldObjects;
using Libs.Animations;
using Libs.Fsm;
using Libs.Movesets;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Composition.Installers
{
    public class PlayerInstanceInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private TransformAnimationTarget _bodyAnimationTarget;
        [SerializeField] private TransformAnimationTarget _handsAnimationTarget;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(gameObject));
            Container.BindInterfacesAndSelfTo<LevelTraverserContext>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelRoomsInfoContext>().FromNew().AsSingle();
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");

            Container.Bind<StateMachine>().FromMethod(() => new MoveSetFactory(Container).Create(Container.Resolve<PlayerConfig>().MoveSetConfig));
            Container.NewSingleInterfacesAndSelf<Domain.Player>();
            Container.NewSingle<PlayerControlStateMediator>();
        }
    }
}