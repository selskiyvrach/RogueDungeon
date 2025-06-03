using Zenject;

namespace Game.Libs.Time
{
    public class TimeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<global::Libs.Time.Time>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTime>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}