using DG.Tweening;
using UnityEngine;

namespace Game.Libs.Camera
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        [SerializeField] private Transform _effectsRoot;
        [SerializeField] private UnityEngine.Camera _camera;
        private Tweener _effectTween;

        public UnityEngine.Camera Camera => _camera;
        public Transform Follow { get; set; }
        public Ray MouseRay => _camera.ScreenPointToRay(Input.mousePosition);
        
        private void LateUpdate()
        {
            if (Follow == null) 
                return;
            
            transform.rotation = Follow.rotation;
            transform.position = Follow.position;
        }

        public void KickPosition(Vector3 punch, float duration)
        {
            _effectTween?.Kill(complete: true);
            _effectTween = _effectsRoot.DOPunchPosition(punch, duration).OnComplete(() => _effectTween = null);
        }
    }
}