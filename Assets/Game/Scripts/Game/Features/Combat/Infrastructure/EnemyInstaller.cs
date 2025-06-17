using Game.Features.Combat.Domain.Enemies;
using Game.Libs.InGameResources;
using Libs.Animations;
using Libs.Movesets;
using Libs.UI.Bars;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private AnimationClipTarget _animationClipTarget;
        [SerializeField] private AnimationClipTarget _hitEffectTarget;
        [SerializeField] private SpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        [SerializeField] private TransformAnimationTarget _transformAnimationTarget;
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Bar _stunDurationBar;

        public override void InstallBindings()
        {
            Container.InstanceSingle(gameObject);
            Container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            Container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);
            Container.InstanceSingle(_transformAnimationTarget);
            Container.NewSingle<MoveSetFactory>();
            Container.NewSingle<IFactory<string, EnemyMove>, EnemyStatesFactory>();
            Container.NewSingle<IEnemyStatesProvider, EnemyStatesProvider>();
            Container.NewSingle<EnemyStateMachine>();

            Container.Bind<EnemyImpactAnimator>().FromSubContainerResolve().ByMethod(container =>
            {
                var config = container.Resolve<EnemyConfig>().HitImpactAnimation.Config;
                container.InstanceSingle(_hitEffectTarget);
                container.InstanceSingle(config.Create(container));
                container.Bind<EnemyImpactAnimator>().AsSingle();
            })
                .AsSingle().NonLazy();
            
            Container.NewSingle<Enemy>();
            
            Container.Bind<ResourceBarPresenter>().FromMethod(ctx =>
                Container.Instantiate<ResourceBarPresenter>(new object[] {ctx.Container.Resolve<Enemy>().Health, _healthBar})).AsCached().NonLazy();
            
            // Container.Bind<ResourceBarPresenter>().FromMethod(ctx =>
            //     Container.Instantiate<ResourceBarPresenter>(new object[] {ctx.Container.Resolve<Enemy>().StunDuration, _healthBar})).AsCached().NonLazy();
        }
    }
}