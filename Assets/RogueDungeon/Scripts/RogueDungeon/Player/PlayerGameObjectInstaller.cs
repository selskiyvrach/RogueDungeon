using Common.DotNetUtils;
using Common.GameObjectMarkers;
using Common.ZenjectUtils;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerGameObjectInstaller : MonoInstaller
    {
        [SerializeField] private CharacterAnimationRootObject _characterAnimationRoot;
        [SerializeField] private WeaponAnimationRootObject _weaponAnimationRoot;
        [SerializeField] private CameraParentObject _cameraParentObject;
        [SerializeField] private PlayerRootObject _playerRootObject;
        
        public void InstallToPlayerContext(DiContainer container)
        {
            container.InstanceSingle(_characterAnimationRoot.ThrowIfNull());
            container.InstanceSingle(_weaponAnimationRoot.ThrowIfNull());
            container.InstanceSingle(_cameraParentObject.ThrowIfNull());
            container.InstanceSingle(_playerRootObject.ThrowIfNull());
        }
    }
}