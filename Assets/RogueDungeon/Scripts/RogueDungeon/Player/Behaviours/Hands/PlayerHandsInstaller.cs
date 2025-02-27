using Common.Animations;
using Common.MoveSets;
using Common.UtilsZenject;
using RogueDungeon.Items;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHandsInstaller : MonoInstaller
    {
        /// <summary>
        /// Sheath/Unsheath moveset
        /// </summary>
        [SerializeField] private MoveSetConfig _sheathUnsheathMoveSetConfig;
        [SerializeField] private AnimationPlayer _handsAnimator;
        [SerializeField] private AnimationPlayer _handHeldItemAnimator;
        [SerializeField] private HandHeldItemPresenter _itemPresenter;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            // helps to break circular dependencies between hands and moves
            container.NewSingleInterfacesAndSelf<HandHeldContext>();
            
            container.InstanceSingle(_itemPresenter);
            container.NewSingle<IFactory<ItemConfig, HandHeldItemPresenter>, ItemPresenterFactory>();
            
            // moveset factory for items
            var itemMovesetFactoryContainer = container.CreateSubContainer();
            itemMovesetFactoryContainer.InstanceSingle<IAnimator>(_handHeldItemAnimator);
            itemMovesetFactoryContainer.NewSingle<IFactory<MoveSetConfig, MoveSetBehaviour>, MoveSetFactory>();
            
            // unsheath moveset
            var unsheathMoveSetContainer = container.CreateSubContainer();
            unsheathMoveSetContainer.InstanceSingle<IAnimator>(_handsAnimator);
            unsheathMoveSetContainer.InstanceSingle(new MoveSetFactory(unsheathMoveSetContainer).Create(_sheathUnsheathMoveSetConfig));

            container.InstanceSingle(new HandsArgs(
                // item presenter factory
                container.Resolve<IFactory<ItemConfig, HandHeldItemPresenter>>(),
                // item moveSet factory
                itemMovesetFactoryContainer.Resolve<IFactory<MoveSetConfig, MoveSetBehaviour>>(),
                // sheath/unsheath moveset
                unsheathMoveSetContainer.Resolve<MoveSetBehaviour>(),
                // context  
                container.Resolve<HandHeldContext>()));
            container.NewSingle<PlayerHandsBehaviour>();

            var hands = container.Resolve<PlayerHandsBehaviour>();
            Container.Bind<PlayerHandsBehaviour>().FromInstance(hands).AsSingle();
            Container.Bind<IHandheldContext>().FromInstance(hands).AsSingle();
        }
    }
}