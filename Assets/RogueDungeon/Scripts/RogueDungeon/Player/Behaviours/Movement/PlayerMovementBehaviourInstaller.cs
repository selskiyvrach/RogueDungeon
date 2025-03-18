using Common.Animations;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementBehaviourInstaller : MonoBehaviour
    {
        [SerializeField] private MoveSetConfig _moveSetConfig;
        [SerializeField] private AnimationClipTarget _animationClipTarget;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            container.InstanceSingle(_moveSetConfig);
            container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            container.InstanceSingle(new MoveSetFactory(container).Create(_moveSetConfig));
            container.NewSingleInterfacesAndSelf<PlayerMovementBehaviour>();
            diContainer.Bind<PlayerMovementBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}