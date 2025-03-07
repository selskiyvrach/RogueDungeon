using Common.Animations;
using Common.MoveSets;
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
            Container.NewSingle<Enemy>();
            Container.Resolve<Enemy>().SetBehaviour(new MoveSetFactory(Container).Create<EnemyMoveSetBehaviour, EnemyMove>(Container.Resolve<EnemyConfig>().MoveSet));
        }
    }
}