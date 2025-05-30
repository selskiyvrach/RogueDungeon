using UnityEngine;
using Zenject;

namespace Game.Features.Gameplay.Domain
{
    public class GameOverScreenInstaller : MonoInstaller
    {
        [SerializeField] private GameOverScreen _gameOverScreen;

        public override void InstallBindings() => 
            _gameOverScreen.Construct(Container.Resolve<Gameplay>());
    }
}