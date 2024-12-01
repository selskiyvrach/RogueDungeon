using UnityEngine;
using Zenject;

namespace Common.GameObjectMarkers
{
    public class GameObjectMarkersInstaller : MonoInstaller
    {
        [SerializeField] private GameObjectMarker[] _markers;

        private void OnValidate() => 
            _markers = FindObjectsOfType<GameObjectMarker>();

        public override void InstallBindings()
        {
            foreach (var marker in _markers) 
                Container.Bind(marker.GetType()).FromInstance(marker);
        }
    }
}