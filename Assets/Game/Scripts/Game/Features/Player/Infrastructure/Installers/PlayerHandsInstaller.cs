using Libs.Animations;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Installers
{
    public class PlayerHandsInstaller : MonoBehaviour
    {
        [SerializeField] private TransformAnimationTarget _rightHandAnimationTarget;
        // [SerializeField] private HandHeldItemView _rightHandItemView;
        // [SerializeField] private HandheldMapView _rightHandMapView;
        
        [SerializeField] private TransformAnimationTarget _leftHandAnimationTarget;
        // [SerializeField] private HandHeldItemView _leftHandItemView;
        // [SerializeField] private HandheldMapView _leftHandMapView;

        public void Install(DiContainer diContainer)
        {
            // var container = diContainer.CreateSubContainer();
            // container.Bind<PlayerHandsBehaviour>().AsSingle();
            // var rightHand = CreateHandBehaviour(container, _rightHandAnimationTarget, _rightHandItemView, _rightHandMapView, isRightHand: true);
            // var leftHand = CreateHandBehaviour(container, _leftHandAnimationTarget, _leftHandItemView, _leftHandMapView, isRightHand: false);
            //
            // container.Resolve<PlayerHandsBehaviour>().SetBehaviours(rightHand, leftHand);
            // diContainer.Bind<PlayerHandsBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }

        // private HandBehaviour CreateHandBehaviour(DiContainer diContainer, TransformAnimationTarget animTarget, HandHeldItemView itemView, HandheldMapView mapView, bool isRightHand)
        // {
            // var container = diContainer.CreateSubContainer();
            // container.InstanceSingle(isRightHand);
            // container.InstanceSingle(itemView);
            // container.InstanceSingle(mapView);
            // container.InstanceSingle(animTarget);
            //
            // container.NewSingle<HandheldItemPresenter>();
            // container.NewSingle<HandheldMapPresenter>();
            //
            // container.NewSingle<MoveSetFactory>();
            // container.NewSingle<ItemMoveSetFactory>();
            // container.NewSingleInterfacesAndSelf<HandBehaviour>();
            // container.NewSingle<HandPresenter>();
            // return container.Resolve<HandBehaviour>();
        // }
    }
}