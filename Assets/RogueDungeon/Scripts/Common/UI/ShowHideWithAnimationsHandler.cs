using DG.Tweening;
using UnityEngine;

namespace Common.UI
{
    public class ShowHideWithAnimationsHandler : ShowHideHandler
    {
        private Sequence _tween;

        public override void Show()
        {
            _tween?.Kill(complete: true);
            
            IsShown = true;
            
            _tween = DOTween.Sequence();
            _tween.Append(transform.DOPunchScale(Vector3.one * .1f, 0.25f));
            _tween.AppendCallback(() => base.Show());
            _tween.Play();
        }

        public override void Hide()
        {
            _tween?.Kill(complete: true);
            
            IsShown = false;

            _tween = DOTween.Sequence();
            _tween.Append(transform.DOPunchScale(Vector3.one * -.1f, 0.25f));
            _tween.AppendCallback(() => base.Hide());
            _tween.Play();
        }
    }
}