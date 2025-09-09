using Libs.Utils.DotNet;
using Zenject;

namespace Game.Libs.UniqueNameGenerator
{
    public class UniqueNameGeneratorInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.Bind<IUniqueNameGenerator>().To<global::Libs.Utils.DotNet.UniqueNameGenerator>().AsSingle();
    }
}