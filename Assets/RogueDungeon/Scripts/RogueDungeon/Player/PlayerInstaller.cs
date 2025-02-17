using Common.UtilsZenject;
using RogueDungeon.Input;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.NewSingle<PlayerInput>();
            Container.NewSingleInterfacesAndSelf<Player>();
            
            Container.AutoResolve<Player>();
        }
    }
}