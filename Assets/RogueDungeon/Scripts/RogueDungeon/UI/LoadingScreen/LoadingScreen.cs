using DG.Tweening;
using UnityEngine;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = .25f;

        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            _canvasGroup.DOFade(0, _fadeDuration);
    }
}