using Common.Animations;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsInstaller : MonoBehaviour
    {
        [SerializeField] private TransformAnimationTarget _rightHandAnimationTarget;
        [SerializeField] private Transform _rightHandItemParent;
        
        [SerializeField] private TransformAnimationTarget _leftHandAnimationTarget;
        [SerializeField] private Transform _leftHandItemParent;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            container.Bind<PlayerHandsBehaviour>().AsSingle();
            var rightHand = CreateHandBehaviour(container, _rightHandAnimationTarget, _rightHandItemParent, isRightHand: true);
            var leftHand = CreateHandBehaviour(container, _leftHandAnimationTarget, _leftHandItemParent, isRightHand: false);
            
            container.Resolve<PlayerHandsBehaviour>().SetBehaviours(rightHand, leftHand);
            diContainer.Bind<PlayerHandsBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }

        private PlayerHandBehaviour CreateHandBehaviour(DiContainer diContainer, TransformAnimationTarget transformAnimationTarget, Transform parent, bool isRightHand)
        {
            var container = diContainer.CreateSubContainer();
            container.InstanceSingle(isRightHand);
            container.InstanceSingle(parent);
            container.InstanceSingle(transformAnimationTarget);
            container.NewSingle<MoveSetFactory>();
            container.NewSingle<HandheldItemFactory>();
            container.NewSingle<PlayerHandBehaviour>();
            return container.Resolve<PlayerHandBehaviour>();
        }
    }
}