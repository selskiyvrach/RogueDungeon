using Common.ZenjectUtils;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _cameraParent;
        
        public override void InstallBindings()
        {
            // Container.Resolve<IGameCamera>().Follow = _cameraParent;
            Container.NewSingleNonLazy<Player>();
        }
    }
}