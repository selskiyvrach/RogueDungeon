using DG.Tweening;
using Game.Features.VFX.App;
using UnityEngine;

namespace Game.Features.VFX.Infrastructure
{
    public class BloodScreen : MonoBehaviour, IBloodScreen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Sequence _tweener;
        
        public void Play()
        {
            _tweener?.Kill();
            _tweener = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1, .1f))
                .Append(_canvasGroup.DOFade(0, .5f).SetEase(Ease.InQuad));
        }
    }
}