using Common.SceneManagement;
using RogueDungeon.SceneManagement;
using UnityEngine;

namespace RogueDungeon.Game
{
    public class GameplaySceneInstaller : SceneInstaller<GameplayScene>
    {
        [SerializeField] private GameplayRootObject _gameplayRootObject;
        
        protected override void InstallSceneBindings() => 
            Container.BindInterfacesTo<GameplayRootObject>().FromInstance(_gameplayRootObject).AsSingle();
    }
}