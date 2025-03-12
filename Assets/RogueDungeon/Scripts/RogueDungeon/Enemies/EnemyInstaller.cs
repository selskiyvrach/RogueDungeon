using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Enemies.States;
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

            Container.NewSingle<IFactory<EnemyStateConfig, EnemyState>, EnemyStatesFactory>();
            Container.NewSingle<EnemyStatesProvider>();
            Container.NewSingle<EnemyStateMachine>();
            
            Container.NewSingle<Enemy>();
        }
    }
}