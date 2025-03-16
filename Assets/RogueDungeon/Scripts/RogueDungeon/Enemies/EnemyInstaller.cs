using Characters;
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
        [SerializeField] private SpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        [SerializeField] private Bar _healthBar;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle(gameObject);
            Container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            Container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);
            Container.NewSingleInterfacesAndSelf<Health>();
            Container.NewSingle<IFactory<EnemyStateConfig, EnemyState>, EnemyStatesFactory>();
            Container.NewSingle<EnemyStatesProvider>();
            Container.NewSingle<EnemyStateMachine>();
            
            Container.NewSingle<IBarViewModel, HealthBarViewModel>();
            Container.Inject(_healthBar);
            
            Container.NewSingle<Enemy>();
        }
    }
}