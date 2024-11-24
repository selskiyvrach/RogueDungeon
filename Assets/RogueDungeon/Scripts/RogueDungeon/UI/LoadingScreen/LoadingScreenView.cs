using Common.Mvvm.View;
using DG.Tweening;
using UnityEngine;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingScreenView : View<ILoadingScreenViewModel>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = .25f;
        
        private ILoadingScreenViewModel _viewModel;

        public override void Initialize(ILoadingScreenViewModel viewModel)
        {
            base.Initialize(viewModel);
            _canvasGroup.alpha = 1;
        }

        protected override void Close() => 
            _canvasGroup.DOFade(0, _fadeDuration).OnComplete(base.Close);
    }
}