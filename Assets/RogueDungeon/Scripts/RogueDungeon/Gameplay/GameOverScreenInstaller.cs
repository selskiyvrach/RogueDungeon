using System;
using Common.UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameOverScreenInstaller : MonoBehaviour, IUiRootInstaller
    {
        [SerializeField] private GameOverScreen _gameOverScreen;
        
        public void Install(DiContainer container)
        {
            container.Inject(_gameOverScreen, new object[]{ new Action(() => container.Resolve<Gameplay>().Restart())});
        }
    }
}