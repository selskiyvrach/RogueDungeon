using Common.Fsm;
using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public class UnsheatherBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.NewSingleInterfaces<UnsheatherBehaviourContext>();
            Container.NewSingle<UnsheatherBehaviour>().WithArguments(new StatesFactoryWithCache(Container));;
        }
    }
}