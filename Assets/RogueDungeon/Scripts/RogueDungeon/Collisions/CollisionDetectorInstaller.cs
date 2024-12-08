using Zenject;

namespace RogueDungeon.Collisions
{
    public class CollisionDetectorInstaller : ScriptableObjectInstaller<CollisionDetectorInstaller>
    {
        public override void InstallBindings() => 
            Container.Bind<ICollisionsDetector>().To<CollisionsDetector>().FromNew().AsSingle();
    }
}