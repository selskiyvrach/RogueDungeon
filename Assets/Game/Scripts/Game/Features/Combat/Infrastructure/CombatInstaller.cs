using Game.Features.Combat.Domain;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class CombatInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AttacksMediator>().AsSingle();
        }
    }
}