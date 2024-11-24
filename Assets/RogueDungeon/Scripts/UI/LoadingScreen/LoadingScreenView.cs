using System;
using DG.Tweening;
using RogueDungeon.UI.Common;
using UniRx;
using UnityEngine;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingScreenView : View<ILoadingProcessViewModel>, ILoadingScreenView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = .25f;
        
        private ILoadingProcessViewModel _viewModel;
        private IDisposable _sub;

        public override void Initialize(ILoadingProcessViewModel viewModel)
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