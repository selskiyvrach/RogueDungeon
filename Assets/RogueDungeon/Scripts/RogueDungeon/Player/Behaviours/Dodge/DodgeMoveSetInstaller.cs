using Common.Animations;
using Common.Behaviours;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeMoveSetInstaller : MonoInstaller
    {
        [SerializeField] private AnimationPlayer _animationPlayer;
        [SerializeField] private MoveSetConfig _dodgeMoveSetConfig;
        
        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle<IAnimator>(_animationPlayer);
            container.InstanceSingle(new MoveSetFactory(container).Create(_dodgeMoveSetConfig));
            container.AutoResolve<MoveSetBehaviour>();
            container.NewSingleAutoResolve<BehaviourInitializer<MoveSetBehaviour>>();
        }
    }
}