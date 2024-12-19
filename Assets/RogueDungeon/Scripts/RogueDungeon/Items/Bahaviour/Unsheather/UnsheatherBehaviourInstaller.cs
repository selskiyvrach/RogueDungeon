using Common.Fsm;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public class UnsheatherBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private CurrentItemVisibleSetter _itemVisibleSetter;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle<ICurrentItemVisibleSetter>(_itemVisibleSetter);
            Container.NewSingleInterfaces<UnsheatherBehaviourContext>();
            Container.NewSingle<UnsheatherBehaviour>().WithArguments(new StatesFactoryWithCache(Container));;
        }
    }
}