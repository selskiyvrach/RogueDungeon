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
        [SerializeField] private AnimationPlayer _animationPlayer;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle(_spriteRenderer);
            Container.InstanceSingle(gameObject);
            Container.NewSingle<Enemy>();
            Container.InstanceSingle<IAnimator>(_animationPlayer);
            Container.Resolve<Enemy>().SetBehaviour(new MoveSetFactory(Container).Create<EnemyMoveSetBehaviour>(Container.Resolve<EnemyConfig>().MoveSet));
        }
    }
}