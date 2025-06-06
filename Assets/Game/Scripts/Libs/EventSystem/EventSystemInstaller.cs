using UnityEngine;
using Zenject;

namespace Libs.EventSystem
{
    public class EventSystemInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.EventSystems.EventSystem _eventSystemPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<UnityEngine.EventSystems.EventSystem>().FromInstance(Instantiate(_eventSystemPrefab)).AsSingle();
        }
    }
}