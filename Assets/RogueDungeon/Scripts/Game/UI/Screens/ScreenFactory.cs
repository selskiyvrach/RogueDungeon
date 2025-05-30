using Libs.UI;
using Libs.Utils.DotNet;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Game.UI.Screens
{
    public class ScreenFactory
    {
        private readonly ScreensSorter _screensSorter;
        private readonly DiContainer _container;

        public ScreenFactory(DiContainer container, ScreensSorter screensSorter)
        {
            _container = container;
            _screensSorter = screensSorter;
        }

        public T Create<T>(T prefab) where T : Screen 
        {
            var screen = _container.InstantiatePrefab(prefab).GetComponent<T>().ThrowIfNull();
            _screensSorter.AddScreen(screen);
            screen.OnDestroyAsObservable().Subscribe(_ => _screensSorter.RemoveScreen(screen));
            return screen;
        }
    }
}