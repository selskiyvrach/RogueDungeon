using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.Common;
using Game.Features.Player.Domain.Behaviours.Hands;
using Game.Libs.WorldObjects;
using Libs.Animations;
using Libs.Fsm;
using Libs.Movesets;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerGameObject _playerGameObject;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerHandsInstaller _handsInstaller;
        [SerializeField] private TransformAnimationTarget _bodyAnimationTarget;
        [SerializeField] private TransformAnimationTarget _handsAnimationTarget;

        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingleInterfacesAndSelf<Domain.Player>();

            Container.NewSingle<PlayerControlStateMediator>();
            
            _handsInstaller.Install(Container);
            
            Container.BindInstance(_bodyAnimationTarget);
            Container.BindInstance(_handsAnimationTarget).WithId("hands");
            Container.InstanceSingle(new MoveSetFactory(Container).Create(_config.MoveSetConfig));
            Container.Bind<StateMachine>().AsSingle();

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<StateMachine>();
            var player = Container.Resolve<Domain.Player>(); 
            player.SetBehaviours(hands, movement);
            _handsInstaller.Initialize();
        }
    }
}