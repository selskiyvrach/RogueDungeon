using UnityEngine;
using Zenject;

namespace Libs.UI
{
    public class CameraToWorldCanvasInjector : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [Inject]
        private void Construct(Camera camera) => 
            _canvas.worldCamera = camera;
    }
}