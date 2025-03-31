using Common.UI;
using Common.UtilsDotNet;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace RogueDungeon.UI
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
            var screen = _container.InstantiatePrefab(prefab).GetComponent<T>().ThrowIfNull();
            _screensSorter.AddScreen(screen);
            screen.OnDestroyAsObservable().Subscribe(_ => _screensSorter.RemoveScreen(screen));
            return screen;
        }
    }
}