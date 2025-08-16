using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Game.Libs.UI
{
    public class ScreensesManager : IScreensService, IScreensRegistry
    {       
        private readonly HashSet<IScreen> _openScreens = new();
        private readonly HashSet<IScreen> _screens = new();
        
        public void Show(IShowRequest request) 
        {
            var screen = _screens.FirstOrDefault(n => n.AcceptsShowRequest(request));
            Assert.IsNotNull(screen);
            Assert.IsFalse(_openScreens.Contains(screen));
            screen.Show(request);
            _openScreens.Add(screen);
        }

        public void Hide(IHideRequest request)
        {
            var screen = _openScreens.FirstOrDefault(n => n.AcceptsHideRequest(request));
            Assert.IsNotNull(screen);
            screen.Hide(request);
            _openScreens.Remove(screen);
        }

        public void Register(IScreen screen)
        {
            Assert.IsFalse(_screens.Contains(screen));
            _screens.Add(screen);
        }

        public void Unregister(IScreen screen)
        {
            Assert.IsTrue(_screens.Contains(screen));
            _screens.Remove(screen);
        }
    }
}