using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.NewSingle<Enemy>();
    }
}