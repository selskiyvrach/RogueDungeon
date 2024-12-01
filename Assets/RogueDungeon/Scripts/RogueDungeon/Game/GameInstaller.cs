using Common.Game;
using Common.InstallerGenerator;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        public override void InstallBindings()
        {
            Container.NewSingle<IGameStatesFactory, GameStateFactory>();
            Container.NewSingle<IGameStateChanger, GameStateChanger>();
            Container.Bind<Common.Game.Game>().To<Common.Game.Game>().FromNew().AsSingle().NonLazy();
        }
    }
}