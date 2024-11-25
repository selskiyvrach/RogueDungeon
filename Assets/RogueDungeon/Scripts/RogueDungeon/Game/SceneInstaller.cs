using Common.Events;
using Common.SceneManagement;
using Zenject;

namespace RogueDungeon.Game
{
    public abstract class SceneInstaller<T> : MonoInstaller where T : Scene
    {
        public sealed override void InstallBindings()
        {
            InstallSceneBindings();
            Container.Resolve<IEventHandler<OnSceneContainerCreatedEvent<T>>>().HandleEvent(new OnSceneContainerCreatedEvent<T>(Container));
        }

        protected virtual void InstallSceneBindings()
        {
        }
    }
}