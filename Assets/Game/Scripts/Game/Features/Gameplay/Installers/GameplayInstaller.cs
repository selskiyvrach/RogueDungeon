using Game.Features.Gameplay.App;
using Zenject;

namespace Game.Features.Gameplay.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Domain.Gameplay>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartGameplayUseCase>().AsSingle().NonLazy();
        }
    }
}