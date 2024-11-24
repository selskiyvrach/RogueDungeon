using System;
using Common.Mvvm.View;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingScreenView : View<ILoadingScreenViewModel>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = .25f;
        
        private ILoadingScreenViewModel _viewModel;
        private IDisposable _sub;

        public override void Initialize(ILoadingScreenViewModel viewModel)
        {
            base.Initialize(viewModel);
            _canvasGroup.alpha = 1;
            _sub = viewModel.IsFinished.Where(n => n).Subscribe(_ => Close());
        }

        private void Close()
        {
            _sub.Dispose();
            _canvasGroup.DOFade(0, _fadeDuration).OnComplete(Dispose);
        }
    }
}