using Common.Fsm;
using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class WeaponBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.NewSingleInterfaces<WeaponBehaviourContext>();
            Container.NewSingle<WeaponBehaviour>().WithArguments(new StatesFactoryWithCache(Container));
        }
    }
}