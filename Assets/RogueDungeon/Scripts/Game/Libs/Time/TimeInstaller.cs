using Zenject;
using ITickable = Libs.Lifecycle.ITickable;

namespace Game.Libs.Time
{
    public class TimeInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.Bind<IFactory<ITickable, TickOrder, ObjectTicker>>().To<ObjectTickerFactory>().AsSingle();
    }
}