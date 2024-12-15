using Common.ZenjectUtils;
using RogueDungeon.Parameters;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        [SerializeField] private ParametersPicker _parameterPickers;
        
        public override void InstallBindings()
        {
            // Container.Resolve<IGameCamera>().Follow = _cameraParent;
            Container.NewSingleNonLazy<Player>();
        }
    }
}