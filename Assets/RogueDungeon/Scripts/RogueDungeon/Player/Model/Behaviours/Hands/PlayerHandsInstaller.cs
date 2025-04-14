using Common.UtilsZenject;
using RogueDungeon.Items;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandsInstaller : MonoBehaviour
    {
        [SerializeField] private ItemAnimationClipTarget _rightHandAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _rightHandItemPresenter;
        
        [SerializeField] private ItemAnimationClipTarget _leftHandAnimationTarget;
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

        private PlayerHandBehaviour CreateHandBehaviour(DiContainer diContainer, ItemAnimationClipTarget itemAnimationTarget, HandHeldItemPresenter itemPresenter)
        {
            var container = diContainer.CreateSubContainer();
            container.NewSingle<PlayerHandBehaviour>();
            container.InstanceSingle(itemPresenter);
            container.InstanceSingle(itemAnimationTarget);
            container.NewSingle<IFactory<IItem, HandHeldItemPresenter>, ItemPresenterFactory>();
            container.NewSingle<ItemMoveSetFactory>();
            return container.Resolve<PlayerHandBehaviour>();
        }
    }
}