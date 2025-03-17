using Common.Animations;
using Common.Fsm;
using Common.MoveSets;
using Common.UtilsZenject;
using RogueDungeon.Items;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHandsInstaller : MonoBehaviour
    {
        /// <summary>
        /// Sheath/Unsheath moveset
        /// </summary>
        [SerializeField] private MoveSetConfig _sheathUnsheathMoveSetConfig;
        [SerializeField] private AnimationClipTarget _handsAnimationsTarget;
        [SerializeField] private AnimationClipTarget _handHeldItemAnimationsTarget;
        [SerializeField] private HandHeldItemPresenter _itemPresenter;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            // helps to break circular dependencies between hands and moves
            container.NewSingleInterfacesAndSelf<HandHeldContext>();
            
            container.InstanceSingle(_itemPresenter);
            container.NewSingle<IFactory<ItemConfig, HandHeldItemPresenter>, ItemPresenterFactory>();
            
            // moveset factory for items
            var itemMovesetFactoryContainer = container.CreateSubContainer();
            itemMovesetFactoryContainer.InstanceSingle<IAnimationClipTarget>(_handHeldItemAnimationsTarget);
            itemMovesetFactoryContainer.NewSingle<IFactory<MoveSetConfig, StateMachine>, MoveSetFactory>();
            
            // unsheath moveset
            var unsheathMoveSetContainer = container.CreateSubContainer();
            unsheathMoveSetContainer.InstanceSingle<IAnimationClipTarget>(_handsAnimationsTarget);
            unsheathMoveSetContainer.InstanceSingle(new MoveSetFactory(unsheathMoveSetContainer).Create(_sheathUnsheathMoveSetConfig));

            container.InstanceSingle(new HandsArgs(
                // item presenter factory
                container.Resolve<IFactory<ItemConfig, HandHeldItemPresenter>>(),
                // item moveSet factory
                itemMovesetFactoryContainer.Resolve<IFactory<MoveSetConfig, StateMachine>>(),
                // sheath/unsheath moveset
                unsheathMoveSetContainer.Resolve<StateMachine>(),
                // context  
                container.Resolve<HandHeldContext>()));
            container.NewSingle<PlayerHandsBehaviour>();

            var hands = container.Resolve<PlayerHandsBehaviour>();
            diContainer.Bind<PlayerHandsBehaviour>().FromInstance(hands).AsSingle();
        }
    }
}