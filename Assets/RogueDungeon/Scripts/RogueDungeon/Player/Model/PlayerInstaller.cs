using Common.Animations;
using Common.MoveSets;
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
        [SerializeField] private AnimationClipTarget _animationTarget;

        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerGameObject);
            Container.InstanceSingle(_config);
            Container.Bind<PlayerPositionInTheMaze>().FromNew().AsSingle().WithArguments<ITwoDWorldObject>(new TwoDWorldObject(_playerGameObject.gameObject));
            Container.NewSingle<Player>();

            Container.NewSingle<PlayerControlStateMediator>();
            
            _handsInstaller.Install(Container);
            
            Container.InstanceSingle<IAnimationClipTarget>(_animationTarget);
            Container.InstanceSingle(new MoveSetFactory(Container).Create(_config.MoveSetConfig));
            Container.Bind<PlayerBehaviour>().AsSingle();

            var hands = Container.Resolve<PlayerHandsBehaviour>();
            var movement = Container.Resolve<PlayerBehaviour>();
            Container.Resolve<Player>().SetBehaviours(hands, movement);
        }
    }
}