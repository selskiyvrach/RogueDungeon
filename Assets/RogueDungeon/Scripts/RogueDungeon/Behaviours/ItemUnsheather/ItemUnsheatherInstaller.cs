using Common.UtilsZenject;
using Zenject;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public class HandheldEquipmentBehaviourInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.NewSingle<HandheldItemBehaviour>();
    }
}