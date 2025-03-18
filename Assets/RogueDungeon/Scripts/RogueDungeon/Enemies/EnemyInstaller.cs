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
    public class EnemyInstaller : MonoBehaviour
    {
        [SerializeField] private AnimationClipTarget _animationClipTarget;
        [SerializeField] private SpriteSheetAnimationTarget _spriteSheetAnimationTarget;
        [SerializeField] private Bar _healthBar;
        
        public Enemy Install(DiContainer container)
        {
            container.InstanceSingle(gameObject);
            container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);
            container.NewSingleInterfacesAndSelf<Health>();
            container.NewSingle<IFactory<EnemyStateConfig, EnemyState>, EnemyStatesFactory>();
            container.NewSingle<EnemyStatesProvider>();
            container.NewSingle<EnemyStateMachine>();
            container.NewSingle<Enemy>();
            
            container.NewSingle<IBarViewModel, EnemyBarViewModel>();
            container.Inject(_healthBar);

            return container.Resolve<Enemy>();
        }
    }
}