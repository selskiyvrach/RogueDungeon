using UnityEngine;
using Zenject;

namespace Libs.UI.Bars
{
    public class BarDeltaConfigInstaller : MonoInstaller
    {
        [SerializeField] private BarDeltaConfig _config;

        public override void InstallBindings() => 
            Container.Bind<BarDeltaConfig>().FromInstance(_config).AsSingle();
    }
}