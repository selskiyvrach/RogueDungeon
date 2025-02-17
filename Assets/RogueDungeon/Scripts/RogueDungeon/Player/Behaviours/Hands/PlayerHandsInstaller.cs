using Common.Animations;
using Common.Behaviours;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHandsInstaller : MonoInstaller
    {
        [SerializeField] private MoveSetConfig _config;
        [SerializeField] private AnimationPlayer _handsAnimator;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.NewSingle<IHandheldContext, PlayerHands>();
            container.InstanceSingle<IAnimator>(_handsAnimator);
            container.InstanceSingle(new MoveSetBehaviourFactory().Create(_config, container));
            container.NewSingleAutoResolve<BehaviourAutorunner<MoveSetBehaviour>>();
            Container.InstanceSingle(container.Resolve<PlayerHands>());
        }
    }
}