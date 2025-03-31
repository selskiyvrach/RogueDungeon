using Common.Animations;
using Common.UI.Bars;
using Common.UtilsZenject;
using RogueDungeon.Enemies.States;
using RogueDungeon.Scripts.RogueDungeon.UI;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private AnimationClipTarget _animationClipTarget;
        [SerializeField] private AnimationClipTarget _hitEffectTarget;
        [SerializeField] private SpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Bar _healthBarDelta;
        [SerializeField] private Bar _stunDurationBar;

        public override void InstallBindings()
        {
            Container.InstanceSingle(gameObject);
            Container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            Container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);
            Container.NewSingle<IFactory<EnemyStateConfig, EnemyState>, EnemyStatesFactory>();
            Container.NewSingle<EnemyStatesProvider>();
            Container.NewSingle<EnemyStateMachine>();
            
            var config = Container.Resolve<EnemyConfig>().HitImpactAnimation.Config;
            Container.InstanceSingle(Container.Instantiate<EnemyImpactAnimator>(new[] { Container.Instantiate(config.AnimationType, new object[]{config, _hitEffectTarget})}));
            
            Container.NewSingle<Enemy>();

            var hpViewModel = Container.Instantiate<EnemyHealthBarViewModel>();
            _healthBar.Construct(hpViewModel);
            _healthBarDelta.Construct(new BarDeltaViewModel(hpViewModel, Container.Resolve<BarDeltaConfig>()));
            _stunDurationBar.Construct(Container.Instantiate<EnemyStunDurationBarViewModel>());
        }
    }
}