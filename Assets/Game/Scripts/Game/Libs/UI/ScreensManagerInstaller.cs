using Zenject;

namespace Game.Libs.UI
{
    public class ScreensManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScreensesManager>().AsSingle();
        }
    }
}