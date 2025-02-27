using Common.UtilsZenject;
using RogueDungeon.Combat;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.InstanceSingle(gameObject);
            Container.NewSingle<Enemy>();
            Container.Bind<IEnemyCombatant>().To<Enemy>().FromResolve();
        }
    }
}