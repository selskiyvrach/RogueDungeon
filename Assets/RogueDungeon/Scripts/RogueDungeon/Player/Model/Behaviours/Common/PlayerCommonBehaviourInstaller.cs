using Common.Animations;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class PlayerCommonBehaviourInstaller : MonoBehaviour
    {
        [SerializeField] private MoveSetConfig _moveSetConfig;
        [SerializeField] private AnimationClipTarget _animationClipTarget;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            container.InstanceSingle(_moveSetConfig);
            container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            // remove move configs from dodge moves
            container.InstanceSingle(new MoveSetFactory(container).Create(_moveSetConfig));
            container.NewSingleInterfacesAndSelf<PlayerCommonBehaviour>();
            diContainer.Bind<PlayerCommonBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}