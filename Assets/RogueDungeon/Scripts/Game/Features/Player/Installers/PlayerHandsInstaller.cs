using Game.Features.Items.Domain;
using Game.Features.Items.View;
using Game.Features.Player.Domain.Behaviours.Hands;
using Game.Features.Player.Presenter;
using Libs.Animations;
using Libs.Movesets;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Installers
{
    public class PlayerHandsInstaller : MonoBehaviour
    {
        [SerializeField] private TransformAnimationTarget _rightHandAnimationTarget;
        [SerializeField] private HandHeldItemView _rightHandItemView;
        [SerializeField] private HandheldMapView _rightHandMapView;
        
        [SerializeField] private TransformAnimationTarget _leftHandAnimationTarget;
        [SerializeField] private HandHeldItemView _leftHandItemView;
        [SerializeField] private HandheldMapView _leftHandMapView;
        private DiContainer _rightHandContainer;
        private DiContainer _leftHandContainer;

        public void Install(DiContainer diContainer)
        {
            var container = diContainer.CreateSubContainer();
            container.Bind<PlayerHandsBehaviour>().AsSingle();
            var rightHand = CreateHandBehaviour(container, _rightHandAnimationTarget, _rightHandItemView, _rightHandMapView, isRightHand: true);
            var leftHand = CreateHandBehaviour(container, _leftHandAnimationTarget, _leftHandItemView, _leftHandMapView, isRightHand: false);
            
            container.Resolve<PlayerHandsBehaviour>().SetBehaviours(rightHand, leftHand);
            diContainer.Bind<PlayerHandsBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }

        public void Initialize()
        {
            _leftHandContainer.Resolve<HandPresenter>();
            _rightHandContainer.Resolve<HandPresenter>();
        }

        private HandBehaviour CreateHandBehaviour(DiContainer diContainer, TransformAnimationTarget animTarget, HandHeldItemView itemView, HandheldMapView mapView, bool isRightHand)
        {
            var container = diContainer.CreateSubContainer();
            container.InstanceSingle(isRightHand);
            container.InstanceSingle(itemView);
            container.InstanceSingle(mapView);
            container.InstanceSingle(animTarget);
            
            container.NewSingle<HandheldItemPresenter>();
            container.NewSingle<HandheldMapPresenter>();
            
            container.NewSingle<MoveSetFactory>();
            container.NewSingle<ItemMoveSetFactory>();
            container.NewSingleInterfacesAndSelf<HandBehaviour>();
            container.NewSingle<HandPresenter>();
            if(isRightHand)
                _rightHandContainer = container;
            else
                _leftHandContainer = container;
            return container.Resolve<HandBehaviour>();
        }
    }
}