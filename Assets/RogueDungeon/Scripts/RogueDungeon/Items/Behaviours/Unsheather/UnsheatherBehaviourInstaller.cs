using Common.Fsm;
using Common.Parameters;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.Behaviours.Unsheather
{
    public class UnsheatherBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private CurrentItemVisibleSetter _itemVisibleSetter;
        [SerializeField] private UnsheatherTimings _timings;
        
        public override void InstallBindings()
        {
            Container.NewSingleParameter<IUnsheathDuration>(() => _timings.UnsheathDuration);
            Container.InstanceSingle<ICurrentItemVisibleSetter>(_itemVisibleSetter);
            Container.NewSingleInterfaces<UnsheatherBehaviourContext>();
            Container.NewSingle<UnsheatherBehaviour>().WithArguments(new StatesFactoryWithCache(Container));;
        }
    }
}