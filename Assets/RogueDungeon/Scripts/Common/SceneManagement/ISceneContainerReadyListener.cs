using Zenject;

namespace Common.SceneManagement
{
    public interface ISceneContainerReadyListener<T> where T : Scene
    {
        void OnSceneContainerReady(DiContainer container);
    }
}