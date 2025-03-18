using UnityEngine;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class GameplayHudFactory : IFactory<GameplayHud>
    {
        private readonly DiContainer _container;

        public GameplayHudFactory(DiContainer container) => 
            _container = container;

        public GameplayHud Create()
        {
            var installer = Object.Instantiate(_container.Resolve<GameplayHudConfig>().InstallerPrefab);
            installer.Install(_container);
            return _container.Resolve<GameplayHud>();
        }
    }
}