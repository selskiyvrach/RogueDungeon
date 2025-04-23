using RogueDungeon.Camera;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventory : MonoBehaviour
    {
        [SerializeField] private WorldInventoryAnimator _animator;
        private static readonly RaycastHit[] Hits = new RaycastHit[10];
        private WorldInventoryItem _currentItem;
        private Vector3 _lastMousePosition;
        private IGameCamera _camera;
        public bool IsOpen => _animator.State == WorldInventoryAnimator.AnimatorState.Open;

        [Inject]
        public void Construct(IGameCamera gameCamera) => 
            _camera = gameCamera;

        public void Unpack() => 
            _animator.Unpack();

        public void Pack() => 
            _animator.Pack();

        public void Tick(float timeDelta)
        {
            if (_animator.State is WorldInventoryAnimator.AnimatorState.Opening
                or WorldInventoryAnimator.AnimatorState.Closing)
            {
                _animator.Tick(timeDelta);
                return;
            }
            
            if(!IsOpen)
                return;

            ScanForItems();
            DragItems();
            _lastMousePosition = UnityEngine.Input.mousePosition;
        }

        private void DragItems()
        {
            if(_currentItem == null)
                return;
            
            if (_currentItem.IsBeingDragged && UnityEngine.Input.GetMouseButtonUp(0))
                _currentItem.IsBeingDragged = false;
            
            if(!_currentItem.IsBeingDragged && UnityEngine.Input.GetMouseButtonDown(0))
                _currentItem.IsBeingDragged = true;

            if (_currentItem.IsBeingDragged)
            {
                var delta = UnityEngine.Input.mousePosition - _lastMousePosition;
                _currentItem.transform.position += new Vector3(delta.x, 0, delta.y) * 0.001f;
            }
        }

        private void ScanForItems()
        {
            if(_currentItem?.IsBeingDragged ?? false)
                return;
            
            var hitCount = Physics.RaycastNonAlloc(_camera.MouseRay, Hits, 10f, LayerMask.GetMask("Default"));
            if (hitCount == 0)
            {
                if (_currentItem == null) 
                    return;
                
                _currentItem.IsPointedAt = false;
                _currentItem.IsBeingDragged = false;
                _currentItem = null;
                return;
            }
            
            var pointerDirection = _camera.MouseRay.direction;
            var closest = Hits[0];
            var closestDot = 0f;
            for (var i = 0; i < hitCount; i++)
            {
                var itemDirection = (Hits[i].collider.transform.position - _camera.MouseRay.origin).normalized;
                var dot = Vector3.Dot(pointerDirection, itemDirection);
                if (dot < closestDot) 
                    continue;
                
                closestDot = dot;
                closest = Hits[i];
            }

            var newItem = closest.collider.gameObject.GetComponent<WorldInventoryItem>();
            if(_currentItem == newItem)
                return;

            if (_currentItem != null)
            {
                _currentItem.IsPointedAt = false;
                _currentItem.IsBeingDragged = false;
                _currentItem = null;
            }

            _currentItem = newItem;
            _currentItem.IsPointedAt = true;
        }
    }
}