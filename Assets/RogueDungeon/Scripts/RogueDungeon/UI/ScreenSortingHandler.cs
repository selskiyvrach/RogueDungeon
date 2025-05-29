using Common.UI;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScreenSortingHandler : MonoBehaviour
    {
        [SerializeField] private Screen _screen;
        private ScreensSorter _sorter;
        
        [Inject]
        private void Construct(ScreensSorter sorter) => 
            _sorter = sorter;

        private void Start() => 
            _sorter.AddScreen(_screen);

        private void OnDestroy() => 
            _sorter.RemoveScreen(_screen);
    }
}