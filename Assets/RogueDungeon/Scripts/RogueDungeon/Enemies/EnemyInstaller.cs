using Common.UtilsZenject;
using RogueDungeon.Combat;
using RogueDungeon.Enemies.Attacks;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] public SpriteRenderer _spriteRenderer;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle(_spriteRenderer);
            Container.InstanceSingle(gameObject);
            Container.NewSingle<Enemy>();
            Container.Bind<IEnemyCombatant>().To<Enemy>().FromResolve();
        }
    }
}