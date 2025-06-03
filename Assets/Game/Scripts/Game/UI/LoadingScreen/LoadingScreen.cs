using DG.Tweening;
using UnityEngine;

namespace Game.UI.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = .25f;

        private void Awake() => 
            gameObject.SetActive(false);

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        public void Hide() => 
            _canvasGroup.DOFade(0, _fadeDuration).OnComplete(() => gameObject.SetActive(false));
    }
}