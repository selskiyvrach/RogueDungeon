using System.Collections.Generic;
using System.Linq;
using Common.UtilsUnity;
using UnityEngine;

namespace Common.UI
{
    public class ScreensSorter : MonoBehaviour
    {
        private readonly List<Screen> _screens = new();

        private void Awake()
        {
            var screens = transform.GetDirectChildren<Screen>().ToArray();
            transform.DetachChildren();
            foreach (var screen in screens) 
                AddScreen(screen);
        }

        public void AddScreen(Screen screen)
        {
            screen.transform.SetParent(transform);
            for (var i = 0; i < _screens.Count; i++)
            {
                if(_screens[i].SortingOrder <= screen.SortingOrder)
                    continue;
                screen.transform.SetSiblingIndex(i);
                break;
            }
            _screens.Add(screen);
        }

        public void RemoveScreen(Screen screen) => 
            _screens.Remove(screen);
    }
}