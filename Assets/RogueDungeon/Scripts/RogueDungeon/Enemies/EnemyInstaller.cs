using Common.Animations;
using Common.MoveSets;
using Common.UI.Bars;
using Common.UtilsZenject;
using Enemies.States;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private AnimationClipTarget _animationClipTarget;
        [SerializeField] private AnimationClipTarget _hitEffectTarget;
        [SerializeField] private SpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        [SerializeField] private TransformAnimationTarget _transformAnimationTarget;
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Bar _healthBarDelta;
        [SerializeField] private Bar _stunDurationBar;

        public override void InstallBindings()
        {
            Container.InstanceSingle(gameObject);
            Container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            Container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);
            Container.InstanceSingle(_transformAnimationTarget);
            Container.NewSingle<MoveSetFactory>();
            Container.NewSingle<IFactory<string, EnemyMove>, EnemyStatesFactory>();
            Container.NewSingle<EnemyStatesProvider>();
            Container.NewSingle<EnemyStateMachine>();

            var config = Container.Resolve<EnemyConfig>().HitImpactAnimation.Config;
            var effectsContainer = Container.CreateSubContainer();
            effectsContainer.InstanceSingle(_hitEffectTarget);
            Container.InstanceSingle(effectsContainer.Instantiate<EnemyImpactAnimator>(new[] { config.Create(effectsContainer)}));
            
            Container.NewSingle<Enemy>();
        }
    }
}