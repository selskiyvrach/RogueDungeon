using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Items.Handling.Unsheather
{
    public class HandheldEquipmentBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.NewSingle<HandheldItemBehaviour>();
    }
}