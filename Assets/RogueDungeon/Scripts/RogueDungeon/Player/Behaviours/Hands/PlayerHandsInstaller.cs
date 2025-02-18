using Common.Animations;
using Common.Behaviours;
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
        [SerializeField] private MoveSetConfig _config;
        [SerializeField] private AnimationPlayer _handsAnimator;
        [SerializeField] private HandHeldItemPresenter _itemPresenter;
        [SerializeField] private ItemConfig _testItemConfig;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.NewSingleInterfacesAndSelf<PlayerHands>();
            container.InstanceSingle<IAnimator>(_handsAnimator);
            container.InstanceSingle(_itemPresenter);
            container.NewSingle<IFactory<ItemConfig, HandHeldItemPresenter>, ItemPresenterFactory>();
            container.NewSingle<IFactory<MoveSetConfig, MoveSetBehaviour>, MoveSetFactory>();
            container.InstanceSingle(new MoveSetFactory(container).Create(_config));
            container.NewSingleAutoResolve<BehaviourAutorunner<MoveSetBehaviour>>();
            Container.InstanceSingle(container.Resolve<PlayerHands>());
            
            container.Resolve<PlayerHands>().IntendedItem = new Item(_testItemConfig);
        }
    }
}