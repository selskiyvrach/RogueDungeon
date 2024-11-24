using UnityEngine;
using Zenject;

namespace RogueDungeon.Collisions
{
    [CreateAssetMenu(menuName = "Installers/CollisionDetector", fileName = "CollisionDetectorInstaller", order = 0)]
    public class CollisionDetectorInstaller : ScriptableObjectInstaller<CollisionDetectorInstaller>
    {
        public override void InstallBindings() => 
            Container.Bind<ICollisionsDetector>().To<CollisionsDetector>().FromNew().AsSingle();
    }
}