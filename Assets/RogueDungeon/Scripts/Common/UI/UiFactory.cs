using UnityEngine;
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
            var screen = Object.Instantiate(prefab);
            screen.GetComponent<IUiRootInstaller>().Install(_container);
            _screensSorter.AddScreen(screen);
            return screen;
        }
    }

    public interface IUiRootInstaller
    {
        void Install(DiContainer container);
    }
}