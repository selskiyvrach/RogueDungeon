using Common.SceneManagement;
using Zenject;

namespace RogueDungeon.Game
{
    public readonly struct OnSceneContainerCreatedEvent<T> where T : Scene
    {
        public readonly DiContainer SceneContainer;

        public OnSceneContainerCreatedEvent(DiContainer sceneContainer) => 
            SceneContainer = sceneContainer;
    }
}