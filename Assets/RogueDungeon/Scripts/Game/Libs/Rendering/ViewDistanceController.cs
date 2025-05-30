using Game.Libs.Camera;
using UnityEngine;

namespace Game.Libs.Rendering
{
    public class ViewDistanceController : MonoBehaviour
    {
        [SerializeField] public float _viewDistance = 2f;
        
        private static readonly float[] _shades = { 1, 1, .85f, .55f, .15f, .05f, 0 };
        private GameCamera _camera;

        private void Start() => 
            _camera = FindObjectOfType<GameCamera>();

        private void LateUpdate()
        {
            var renderers = FindObjectsOfType<SpriteRenderer>();
            foreach (var spriteRenderer in renderers)
            {
                var c = spriteRenderer.color;
                c.a = _shades[Mathf.RoundToInt(Mathf.Lerp(0, 1, Vector3.Distance(spriteRenderer.transform.position, _camera.transform.position) / _viewDistance) * (_shades.Length - 1))];
                spriteRenderer.color = c;
            }
        }
    }
}