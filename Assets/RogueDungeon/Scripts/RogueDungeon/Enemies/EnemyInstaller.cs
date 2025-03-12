using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Enemies.MoveSet;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private AnimationClipTarget _animationClipTarget;
        [SerializeField] private SpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle(gameObject);
            Container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            Container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);

            var idleState = Container.Resolve<EnemyConfig>().IdleState;
            Container.NewSingle<IFactory<EnemyStateConfig, EnemyState>, EnemyStatesFactory>();
            Container.Bind<EnemyStateMachine>().FromNew().AsSingle().WithArguments(idleState);
            
            Container.NewSingle<Enemy>();
        }
    }
}