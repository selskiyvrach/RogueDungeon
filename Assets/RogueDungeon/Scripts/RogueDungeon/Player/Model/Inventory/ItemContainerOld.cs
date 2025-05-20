// using System;
// using System.Collections.Generic;
// using Common.Unity;
// using Common.UtilsDotNet;
// using ModestTree;
// using RogueDungeon.Camera;
// using Sirenix.OdinInspector;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
// using Zenject;
//
// namespace RogueDungeon.Player.Model.Inventory
// {
//     // not every placeable place requires inventory dependency
//
//     [RequireComponent(typeof(GraphicRaycaster)), 
//      RequireComponent(typeof(RectTransform))]
//     public class ItemContainerOld : MonoBehaviour
//     {
//         private enum Type
//         {
//             None,
//             Plain,
//             Slot,
//             Grid
//         }
//         
//         private static readonly List<RaycastResult> Hits = new(15);
//         private static readonly List<ItemView> Items = new(15);
//         
//         [SerializeField] private Type _type;
//         [SerializeField, HideInInspector] private GraphicRaycaster _raycaster;
//         [SerializeField, ShowIf("@_type==Type.Grid")] private GridLayoutGroup _gridLayout;
//         private PointerEventData _pointer;
//         private EventSystem _eventSystem;
//         private Inventory  _inventory;
//
//         [field: SerializeField, HideInInspector] public RectTransform RectTransform { get; private set; }
//         [field: SerializeField, ShowIf("@_type==Type.Slot")] public SlotType SlotType { get; private set; }
//
//         public float CellSize => _type switch
//         {
//             Type.Plain => 30,
//             Type.Slot => 20,
//             Type.Grid => GetCellSize(),
//             _ => throw new ArgumentOutOfRangeException()
//         };
//
//         [Inject]
//         private void Construct(EventSystem eventSystem, IGameCamera gameCamera, Inventory inventory)
//         {
//             _inventory = inventory;
//             // _camera = gameCamera.Camera;
//             _eventSystem = eventSystem;
//             _pointer = new PointerEventData(_eventSystem);
//         }
//
//         private void OnValidate()
//         {
//             RectTransform = GetComponent<RectTransform>().ThrowIfNull();
//             _raycaster = GetComponent<GraphicRaycaster>().ThrowIfNull();
//         }
//
//         public bool TryProjectItem(ItemView item, Vector3 screenPos, out Vector3 worldPos, out bool canBePlaced, out ItemView replacedItem)
//         {
//             worldPos = default;
//             canBePlaced = false;
//             replacedItem = null;
//             
//             if(!RectTransformUtility.RectangleContainsScreenPoint(RectTransform, screenPos, _camera) || 
//                !RectTransformUtility.ScreenPointToWorldPointInRectangle(RectTransform, screenPos, _camera, out Vector3 point))
//                 return false;
//
//             if (_type == Type.Grid)
//             {
//                 worldPos = GetGridProjection(item, screenPos, out canBePlaced, out var replacedItemId);
//                 if(replacedItemId != -1)
//                     replacedItem = Items[replacedItemId];
//             }
//
//             else
//             {
//                 // canBePlaced = true;
//                 // worldPos = _type switch
//                 // {
//                 //     Type.Plain => point,
//                 //     Type.Slot => transform.position,
//                 //     _ => throw new ArgumentOutOfRangeException()
//                 // };
//             }
//             return true;
//         }
//
//         public bool TryRaycastItem(Vector3 screenPos, UnityEngine.Camera camera, out ItemView item)
//         {
//             item = null;
//             Hits.Clear();
//             _pointer.Reset();
//             _pointer.position = screenPos;
//             _raycaster.Raycast(_pointer, Hits);
//              
//             Items.Clear();
//             foreach (var hit in Hits)
//             {
//                 if(hit.gameObject.GetComponent<ItemView>() is {} itemComponent)
//                     Items.Add(itemComponent);
//             }
//             
//             if (Items.Count == 0)
//                 return false;
//             
//             item = Items[0];
//             var closestDistance = float.PositiveInfinity;
//             foreach (var i in Items)
//             {
//                 var itemPos = camera.WorldToScreenPoint(i.transform.position);
//                 var distance = (screenPos - itemPos).magnitude;
//                 if (distance < closestDistance) 
//                     continue;
//                 
//                 closestDistance = distance;
//                 item = i;
//             }
//             return true;
//         }
//
//         private Vector3 GetGridProjection(ItemView item, Vector3 screenPos, out bool canBePlaced, out int replaceItemId)
//         {
//             canBePlaced = false;
//             replaceItemId = -1;
//             RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, screenPos, _camera, out var localPoint);
//             localPoint += RectTransform.sizeDelta / 2f;
//             
//             foreach (var child in _gridLayout.transform.GetDirectChildren<RectTransform>())
//                 child.GetComponentInChildren<Image>().enabled = false;
//             
//             var halfCell = Vector2.one * CellSize / 2f;
//             
//             if (!IsPointProjected(localPoint - (item.Size / 2f - halfCell), out var corner1) ||
//                 !IsPointProjected(localPoint + (item.Size / 2f - halfCell), out var corner2)) 
//                 return localPoint;
//             
//             canBePlaced = true;
//             return Vector3.Lerp(corner1, corner2, .5f);
//             return Vector3.zero;
//         }
//
//         private bool IsPointProjected(Vector2 point, out Vector3 projectedPoint)
//         {
//             projectedPoint = Vector3.zero;
//             _gridLayout.GetRowsAndColumns(out var rows, out var columns);
//
//             var cellRow = Mathf.FloorToInt( (1 - point.y / RectTransform.sizeDelta.y) * rows);
//             var cellColumn = Mathf.FloorToInt(point.x / RectTransform.sizeDelta.x * columns);
//
//             var row = -1;
//             var column = -1;
//             foreach (var child in _gridLayout.transform.GetDirectChildren<RectTransform>())
//             {
//                 column++;
//                 column %= columns;
//                 if (column == 0)
//                     row++;
//                 var cellInQuestion = row == cellRow && column == cellColumn;
//                 if (!cellInQuestion) 
//                     continue;
//                 
//                 child.GetComponentInChildren<Image>().enabled = true;
//                 projectedPoint = child.position;
//                 return true;
//             }
//             return false;
//         }
//
//         private float GetCellSize() => 
//             _gridLayout.cellSize.x;
//
//     }
//     
//     public class SlotPlaceablePlace : PlaceablePlaceBase
//     {
//         [SerializeField] private SlotType _slotType;
//         private CursorDetector _cursorDetector;
//         private Inventory _inventory;
//         private ItemView _item;
//
//         [Inject]
//         private void Construct(Inventory inventory, CursorDetector cursorDetector)
//         {
//             _inventory = inventory;
//             _cursorDetector = cursorDetector;
//         }
//
//         public override ItemView ScanForItem(Vector3 screenPos) => 
//             _cursorDetector.RectContainsCursor(RectTransform) 
//                 ? _item 
//                 : null;
//
//         public override bool TryProjectItem(ItemView item, Vector3 screenPos, out Vector3 worldPos, out bool canBePlaced, out int replacedItemId)
//         {
//             throw new NotImplementedException();
//         }
//     }
//
//     
//     [RequireComponent(typeof(RectTransform))]
//     public abstract class PlaceablePlaceBase : MonoBehaviour
//     {
//         protected RectTransform RectTransform;
//
//         protected virtual void OnValidate() => 
//             RectTransform = GetComponent<RectTransform>().ThrowIfNull();
//
//         public abstract ItemView ScanForItem(Vector3 screenPos);
//         public abstract bool TryProjectItem(ItemView item, Vector3 screenPos, out Vector3 worldPos, out bool canBePlaced, out int replacedItemId);
//     }
// }