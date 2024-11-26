using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerGameObjectInstaller : MonoInstaller
    {
        [SerializeField] private PlayerRootGameObject _playerRootGameObject;
        
        public void InstallToPlayerContext(DiContainer container) => 
            container.BindInterfacesTo<PlayerRootGameObject>().FromInstance(_playerRootGameObject);
    }
}