using System;
using System.Collections.Generic;
using Common.UtilsDotNet;
using RogueDungeon.Camera;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    [RequireComponent(typeof(GraphicRaycaster)), 
     RequireComponent(typeof(RectTransform))]
    public class PlaceablePlace : MonoBehaviour
    {
        private enum Type
        {
            None,
            Plain,
            Slot,
            Grid
        }

        private static readonly List<RaycastResult> Hits = new(15);
        private static readonly List<WorldInventoryItem> Items = new(15);
        
        [SerializeField] private Type _type;
        [SerializeField, HideInInspector] private GraphicRaycaster _raycaster;
        [SerializeField, ShowIf("@_type==Type.Grid")] private Vector2Int _size;
        [SerializeField, ShowIf("@_type==Type.Grid")] private GameObject _cellPrefab;
        [SerializeField, ShowIf("@_type==Type.Grid")] private GridLayoutGroup _gridLayout;
        private UnityEngine.Camera _camera;
        private PointerEventData _pointer;
        private EventSystem _eventSystem;

        [field: SerializeField, HideInInspector] public RectTransform RectTransform { get; private set; }

        [Inject]
        private void Construct(EventSystem eventSystem, IGameCamera gameCamera)
        {
            _camera = gameCamera.Camera;
            _eventSystem = eventSystem;
            _pointer = new PointerEventData(_eventSystem);
            
            if(_type == Type.Grid)
                FillGrid();
        }

        private void OnValidate()
        {
            RectTransform = GetComponent<RectTransform>().ThrowIfNull();
            _raycaster = GetComponent<GraphicRaycaster>().ThrowIfNull();
        }

        public bool TryProjectItem(WorldInventoryItem item, Vector3 screenPos, out Vector3 worldPos, out bool canBePlaced)
        {
            worldPos = default;
            canBePlaced = false;
            
            if(!RectTransformUtility.RectangleContainsScreenPoint(RectTransform, screenPos, _camera) || 
               !RectTransformUtility.ScreenPointToWorldPointInRectangle(RectTransform, screenPos, _camera, out Vector3 point))
                return false;
                
            canBePlaced = _type switch
            {
                _ => true,
            };
            worldPos = _type switch
            {
                Type.Plain => point,
                Type.Slot => transform.position,
                Type.Grid => GetGridProjection(item, point),
                _ => throw new ArgumentOutOfRangeException()
            };
            return true;
        }

        private Vector3 GetGridProjection(WorldInventoryItem item, Vector3 worldPoint)
        {
            return new Vector3();
        }

        public WorldInventoryItem ScanForItem(Vector3 screenPos)
        {
            Hits.Clear();
            _pointer.Reset();
            _pointer.position = screenPos;
            _raycaster.Raycast(_pointer, Hits);
             
            Items.Clear();
            foreach (var hit in Hits)
            {
                if(hit.gameObject.GetComponent<WorldInventoryItem>() is {} item)
                    Items.Add(item);
            }
            
            if (Items.Count == 0)
                return null;
            
            var closest = Items[0];
            var closestDistance = float.PositiveInfinity;
            foreach (var item in Items)
            {
                var itemPos = _camera.WorldToScreenPoint(item.transform.position);
                var distance = (screenPos - itemPos).magnitude;
                if (distance < closestDistance) 
                    continue;
                
                closestDistance = distance;
                closest = item;
            }
            return closest;
        }

        private void FillGrid()
        {
            var cellCount = _size.x * _size.y;
            for (var i = 0; i < cellCount; i++) 
                Instantiate(_cellPrefab, _gridLayout.transform);

            var areaSize = _gridLayout.GetComponent<RectTransform>().sizeDelta;
            var cellSize = new Vector2(areaSize.x / _size.x, areaSize.y / _size.y);
            _gridLayout.cellSize = cellSize;
        }
    }
}