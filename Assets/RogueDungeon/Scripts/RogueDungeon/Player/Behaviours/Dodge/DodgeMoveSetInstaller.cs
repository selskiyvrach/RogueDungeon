using Common.Animations;
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeMoveSetInstaller : MonoInstaller
    {
        [SerializeField] private AnimationClipTarget _animationClipTarget;
        [SerializeField] private MoveSetConfig _dodgeMoveSetConfig;
        
        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            container.InstanceSingle(new MoveSetFactory(container).Create(_dodgeMoveSetConfig));
        }
    }
}