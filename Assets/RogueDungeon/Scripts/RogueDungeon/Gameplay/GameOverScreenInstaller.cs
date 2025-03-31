using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameOverScreenInstaller : MonoInstaller
    {
        [SerializeField] private GameOverScreen _gameOverScreen;

        public override void InstallBindings() => 
            _gameOverScreen.Construct(Container.Resolve<Gameplay>());
    }
}