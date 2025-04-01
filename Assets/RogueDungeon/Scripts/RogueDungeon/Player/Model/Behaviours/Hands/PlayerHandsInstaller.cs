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
        [SerializeField] private MoveSetConfig _sheathUnsheathRightHand;
        [SerializeField] private AnimationClipTarget _rightHandAnimationTarget;
        [SerializeField] private AnimationClipTarget _rightHandItemAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _rightHandItemPresenter;
        
        [SerializeField] private MoveSetConfig _sheathUnsheathLeftHand;
        [SerializeField] private AnimationClipTarget _leftHandAnimationTarget;
        [SerializeField] private AnimationClipTarget _leftHandItemAnimationTarget;
        [SerializeField] private HandHeldItemPresenter _leftHandItemPresenter;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            container.Bind<PlayerHandsBehaviour>().AsSingle();
            var rightHand = CreateHandBehaviour(container, _rightHandAnimationTarget, _rightHandItemAnimationTarget, _rightHandItemPresenter, _sheathUnsheathRightHand);
            var leftHand = CreateHandBehaviour(container, _leftHandAnimationTarget, _leftHandItemAnimationTarget, _leftHandItemPresenter, _sheathUnsheathLeftHand);
            
            container.Resolve<PlayerHandsBehaviour>().SetBehaviours(rightHand, leftHand);
            diContainer.Bind<PlayerHandsBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }

        private PlayerHandBehaviour CreateHandBehaviour(DiContainer diContainer, AnimationClipTarget handAnimationTarget, 
            AnimationClipTarget itemAnimationTarget, 
            HandHeldItemPresenter itemPresenter, 
            MoveSetConfig sheathConfig)
        {
            var container = diContainer.CreateSubContainer();
            
            // moveset factory for items
            var itemMovesetFactoryContainer = container.CreateSubContainer();
            itemMovesetFactoryContainer.InstanceSingle<IAnimationClipTarget>(itemAnimationTarget);
            itemMovesetFactoryContainer.NewSingle<ItemMoveSetFactory>();
            
            // item presenter
            container.Bind<IFactory<IItem, HandHeldItemPresenter>>().To<ItemPresenterFactory>().AsSingle().WithArguments(itemPresenter);
            
            // hand
            var factory = itemMovesetFactoryContainer.Resolve<ItemMoveSetFactory>();
            container.Bind<PlayerHandBehaviour>().AsSingle().WithArguments(new object[]{ factory });
            
            // unsheath moveset
            var unsheathMoveSetContainer = container.CreateSubContainer();
            unsheathMoveSetContainer.InstanceSingle<IAnimationClipTarget>(handAnimationTarget);
            unsheathMoveSetContainer.InstanceSingle(new MoveSetFactory(unsheathMoveSetContainer).Create(sheathConfig));
            
            var hand = container.Resolve<PlayerHandBehaviour>();
            hand.SetUnsheathBehaviour(unsheathMoveSetContainer.Resolve<StateMachine>());
            return hand;
        }
    }
}