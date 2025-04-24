using Common.Unity;
using RogueDungeon.Camera;
using RogueDungeon.Levels;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventory : MonoBehaviour
    {
        [SerializeField] private WorldInventoryAnimator _animator;
        private static readonly RaycastHit[] Hits = new RaycastHit[10];
        private WorldInventoryItem _currentItem;
        private IGameCamera _camera;
        private Level _level;
        public bool IsOpen => _animator.State == WorldInventoryAnimator.AnimatorState.Open;

        [Inject]
        public void Construct(IGameCamera gameCamera, Level level)
        {
            _level = level;
            _camera = gameCamera;
        }

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
            DragItem();
        }

        private void DragItem()
        {
            if(_currentItem == null)
                return;

            if (_currentItem.IsBeingDragged && UnityEngine.Input.GetMouseButtonUp(0)) 
                _currentItem.IsBeingDragged = false;

            if (!_currentItem.IsBeingDragged && UnityEngine.Input.GetMouseButtonDown(0)) 
                _currentItem.IsBeingDragged = true;

            if (_currentItem.IsBeingDragged)
            {
                // item is placed at the cursor position
                // shadow is placed at the current legal position
                
                // what is the cursor position 
                    // raycast from screen pos to the inventory canvas background ("DraggableSpace")
                    // stays at the last pos if no legal space found this frame

                    if (Physics.Raycast(_camera.MouseRay, out RaycastHit hit, 100f,
                            LayerMask.GetMask("DraggableSpace")))
                    {
                        _currentItem.Position = hit.point;
                        Debug.Log($"Point: {hit.point}, Pos: {_currentItem.Position}");
                    }

            }
            return;

            Vector2 RotateDelta(Vector2 delta)
            {
                var playerRotation = _level.LevelTraverser.Rotation.Round(); 
                return playerRotation == Vector2Int.up 
                    ? new Vector2(delta.x, delta.y) 
                    : playerRotation == Vector2Int.down 
                        ? new Vector2(-delta.x, -delta.y) 
                        : playerRotation == Vector2Int.left 
                            ? new Vector2(-delta.y, delta.x) 
                            : new Vector2(delta.y,- delta.x);
            }
        }

        private void ScanForItems()
        {
            if(_currentItem?.IsBeingDragged ?? false)
                return;
            
            var hitCount = Physics.RaycastNonAlloc(_camera.MouseRay, Hits, 10f, LayerMask.GetMask("Draggables"));
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