using Common.Animations;
using Common.Fsm;
using Common.MoveSets;
using Common.UtilsZenject;
using RogueDungeon.Items;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsInstaller : MonoBehaviour
    {
        [SerializeField] private AnimationClipTarget _rightHandAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _rightHandItemPresenter;
        
        [SerializeField] private AnimationClipTarget _leftHandAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _leftHandItemPresenter;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            container.Bind<PlayerHandsBehaviour>().AsSingle();
            var rightHand = CreateHandBehaviour(container, _rightHandAnimationTarget, _rightHandItemPresenter);
            var leftHand = CreateHandBehaviour(container, _leftHandAnimationTarget, _leftHandItemPresenter);
            
            container.Resolve<PlayerHandsBehaviour>().SetBehaviours(rightHand, leftHand);
            diContainer.Bind<PlayerHandsBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }

        private PlayerHandBehaviour CreateHandBehaviour(DiContainer diContainer, AnimationClipTarget itemAnimationTarget, HandHeldItemPresenter itemPresenter)
        {
            var container = diContainer.CreateSubContainer();
            container.NewSingle<PlayerHandBehaviour>();
            container.InstanceSingle(itemPresenter);
            container.InstanceSingle<IAnimationClipTarget>(itemAnimationTarget);
            container.NewSingle<IFactory<IItem, HandHeldItemPresenter>, ItemPresenterFactory>();
            container.NewSingle<ItemMoveSetFactory>();
            return container.Resolve<PlayerHandBehaviour>();
        }
    }
}