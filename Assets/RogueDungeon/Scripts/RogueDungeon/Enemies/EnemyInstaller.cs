using Common.UtilsZenject;
using RogueDungeon.Combat;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.NewSingle<Enemy>();
            Container.Bind<IEnemyCombatant>().To<Enemy>().FromResolve();
        }
    }
}