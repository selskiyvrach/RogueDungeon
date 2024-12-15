using Common.Fsm;
using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class WeaponBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
             Container.NewSingle<IStatesFactory, StatesFactoryWithCache>();
             Container.NewSingleInterfacesAndSelf<WeaponBehaviour>();
        }
    }
}