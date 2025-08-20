using Libs.Utils.DotNet;
using Zenject;

namespace Game.Libs.UniuqueNameGenerator
{
    public class UniqueNameGeneratorInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.Bind<IUniqueNameGenerator>().To<UniqueNameGenerator>().AsSingle();
    }
}