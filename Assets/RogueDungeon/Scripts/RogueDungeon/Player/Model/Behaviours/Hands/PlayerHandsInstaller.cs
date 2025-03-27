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
        [SerializeField] private MoveSetConfig _sheathUnsheathMoveSetConfig;
        
        [SerializeField] private AnimationClipTarget _rightHandAnimationTarget;
        [SerializeField] private AnimationClipTarget _rightHandItemAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _rightHandItemPresenter;
        
        [SerializeField] private AnimationClipTarget _leftHandAnimationTarget;
        [SerializeField] private AnimationClipTarget _leftHandItemAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _leftHandItemPresenter;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            var rightHand = CreateHandBehaviour(container, _rightHandAnimationTarget, _rightHandItemAnimationTarget, _rightHandItemPresenter);
            var leftHand = CreateHandBehaviour(container, _leftHandAnimationTarget, _leftHandItemAnimationTarget, _leftHandItemPresenter);
            
            container.Bind<PlayerHandsBehaviour>().AsSingle().WithArguments(rightHand, leftHand);
            diContainer.Bind<PlayerHandsBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }

        private PlayerHandBehaviour CreateHandBehaviour(DiContainer diContainer, AnimationClipTarget handAnimationTarget, AnimationClipTarget itemAnimationTarget, HandHeldItemPresenter itemPresenter)
        {
            var container = diContainer.CreateSubContainer();
            container.NewSingle<PlayerHandBehaviour>();
            
            container.InstanceSingle(itemPresenter);
            container.NewSingle<IFactory<IItem, HandHeldItemPresenter>, ItemPresenterFactory>();
            
            // moveset factory for items
            var itemMovesetFactoryContainer = container.CreateSubContainer();
            itemMovesetFactoryContainer.InstanceSingle<IAnimationClipTarget>(itemAnimationTarget);
            itemMovesetFactoryContainer.NewSingle<ItemMoveSetFactory>();
            
            // unsheath moveset
            var unsheathMoveSetContainer = container.CreateSubContainer();
            unsheathMoveSetContainer.InstanceSingle<IAnimationClipTarget>(handAnimationTarget);
            unsheathMoveSetContainer.InstanceSingle(new MoveSetFactory(unsheathMoveSetContainer).Create(_sheathUnsheathMoveSetConfig));
            
            container.InstanceSingle(itemMovesetFactoryContainer.Resolve<ItemMoveSetFactory>());
            var hand = container.Resolve<PlayerHandBehaviour>();
            hand.SetUnsheathBehaviour(unsheathMoveSetContainer.Resolve<StateMachine>());
            return hand;
        }
    }
}