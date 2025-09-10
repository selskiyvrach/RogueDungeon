using DG.Tweening;
using Libs.Utils.Unity;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Features.VFX.Infrastructure
{
    public class HitFlasher : MonoBehaviour, IHitFlasher
    {
        [SerializeField] private Image _flash;
        
        private IHitFlasherConfig _config;
        private Sequence _sequence;

        private void Awake()
        {
            _flash.gameObject.SetActive(false);
            _flash.SetAlpha(0);
        }

        [Inject]
        public void Construct(IHitFlasherConfig config) => 
            _config = config;

        public void Play(HitFlashPosition pos)
        {
            _flash.gameObject.SetActive(true);
            _flash.transform.localPosition = _config.GetFlashLocalPosition(pos);
            _flash.rectTransform.sizeDelta = _flash.rectTransform.sizeDelta.normalized * _config.GetFlashSize(pos);
            _flash.SetAlpha(0);
                
            _sequence?.Kill(complete: true);
            _sequence = DOTween.Sequence();
            _sequence
                .Append(_flash.DOFade(1, _config.FlashInDuration))
                .Join(_flash.rectTransform.DOPunchScale(Vector3.one * _config.GetTravelDistance(pos), _config.FlashInDuration + _config.FlashOutDuration))
                .Append(_flash.DOFade(0, _config.FlashOutDuration))
                .AppendCallback(() => _flash.gameObject.SetActive(false)).SetEase(Ease.OutQuad);
        }
    }
}