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
        [SerializeField] private Bar _poiseBar;
        
        public Enemy Install(DiContainer container)
        {
            container.InstanceSingle(gameObject);
            container.InstanceSingle<IAnimationClipTarget>(_animationClipTarget);
            container.InstanceSingle<ISpriteSheetAnimationTarget>(_spriteSheetAnimationTarget);
            container.NewSingle<IFactory<EnemyStateConfig, EnemyState>, EnemyStatesFactory>();
            container.NewSingle<EnemyStatesProvider>();
            container.NewSingle<EnemyStateMachine>();
            container.NewSingle<Enemy>();
            
            container.NewSingle<IBarViewModel, EnemyHealthBarViewModel>();
            container.Inject(_healthBar);
            container.Unbind<IBarViewModel>();

            container.NewSingle<IBarViewModel, EnemyPoiseBarViewModel>();
            container.Inject(_poiseBar);
            container.Unbind<IBarViewModel>();            

            return container.Resolve<Enemy>();
        }
    }
}