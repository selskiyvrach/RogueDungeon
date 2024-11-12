using Zenject;

namespace RogueDungeon.Gameplay
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EntitiesRegistry>().FromNew().AsSingle();
        }
    }
}