using Zenject;

namespace Common.SceneManagement
{
    public abstract class SceneInstaller<T> : MonoInstaller where T : Scene
    {
        public sealed override void InstallBindings()
        {
            InstallSceneBindings();
            Container.Resolve<ISceneContainerReadyListener<T>>().OnSceneContainerReady(Container);
        }

        protected virtual void InstallSceneBindings()
        {
        }
    }
}