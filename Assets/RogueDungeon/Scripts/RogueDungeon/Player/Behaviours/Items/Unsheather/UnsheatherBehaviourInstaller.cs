using Common.Animations;
using Common.Behaviours;
using Common.Parameters;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Items.Unsheather
{
    public class UnsheatherBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private CurrentItemVisibleSetter _itemVisibleSetter;
        [SerializeField] private UnsheatherTimings _timings;
        [SerializeField] private AnimationPlayer _animationPlayer;
        
        public override void InstallBindings()
        {
            var subContainer = Container.BehaviourSubcontainer<UnsheatherBehaviour, UnsheatherInternalFacade, UnsheatherExternalFacade>(autoRunBehaviour: true);
            subContainer.NewSingleParameter<IUnsheathDuration>(() => _timings.UnsheathDuration);
            subContainer.InstanceSingle<ICurrentItemVisibleSetter>(_itemVisibleSetter);
            subContainer.InstanceSingleInterfaces(_animationPlayer);
        }
    }
}