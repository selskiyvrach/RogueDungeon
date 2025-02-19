using Common.Animations;
using Common.UtilsZenject;
using RogueDungeon.Input;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private AnimationPlayer _animationPlayer;

        public override void InstallBindings()
        {
            Container.InstanceSingleInterfaces(_animationPlayer);
            Container.NewSingle<IPlayerInput, PlayerInput>();
            Container.NewSingleInterfacesAndSelf<Player>();
        }
    }
}