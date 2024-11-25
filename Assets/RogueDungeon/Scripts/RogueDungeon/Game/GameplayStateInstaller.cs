using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/GameplayInstaller", fileName = "GameplayInstaller", order = 0)]
    public class GameplayStateInstaller : ScriptableObjectInstaller<GameplayStateInstaller>
    {
        public void InstallToSceneContext(DiContainer container)
        {
            
        }
    }
}