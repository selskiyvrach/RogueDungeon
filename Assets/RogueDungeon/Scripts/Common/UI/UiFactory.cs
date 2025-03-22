using Zenject;

namespace Common.UI
{
    public class UiFactory
    {
        private readonly ScreensSorter _screensSorter;
        private readonly DiContainer _container;

        public UiFactory(DiContainer container, ScreensSorter screensSorter)
        {
            _container = container;
            _screensSorter = screensSorter;
        }

        public T Create<T>(T prefab) where T : Screen
        {
            var screen = _container.InstantiatePrefab(prefab).GetComponent<T>();
            _screensSorter.AddScreen(screen);
            return screen;
        }
    }
}