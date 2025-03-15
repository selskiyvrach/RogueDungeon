using Common.Animations;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private MoveSetConfig _moveSetConfig;
        [SerializeField] private AnimationClipTarget _animationClipTarget;

        public override void InstallBindings()
        {
            Container.NewSingle<IDodger, DodgeContext>();
            var container = Container.CreateSubContainer();
            container.InstanceSingle(_moveSetConfig);
            container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            container.InstanceSingle(new MoveSetFactory(container).Create(_moveSetConfig));
            container.NewSingleInterfacesAndSelf<PlayerMovementBehaviour>();
            Container.Bind<PlayerMovementBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}