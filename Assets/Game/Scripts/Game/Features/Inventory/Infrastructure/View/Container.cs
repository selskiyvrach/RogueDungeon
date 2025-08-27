using Game.Features.Inventory.App.Presenters;
using Game.Libs.UI;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public abstract class Container : HoverableGraphic, IContainerView
    {
        [SerializeField, HideInInspector] private RectTransform _rectTransform;
        [field: SerializeField] public float CellSize { get; private set; } = 40;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _rectTransform = GetComponent<RectTransform>().ThrowIfNull();
        }

        public void PlaceItem(IItemView item, Vector2 posNormalized)
        {
            var localPos = (posNormalized - Vector2.one * .5f) * _rectTransform.sizeDelta;
            var itemPos = transform.position + new Vector3(localPos.x, 0, localPos.y);
            item.SetPosition(itemPos);
            item.ProjectionView.SetPosition(itemPos);
            item.SetCellSize(CellSize);
        }

        public Vector2 ScreenPosToLocalPosNormalized(Vector2 point, Camera cam)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, point, cam, out var position);
            return (position + _rectTransform.sizeDelta / 2) / _rectTransform.sizeDelta;
        }

        public Vector3 LocalPosNormalizedToWorldPos(Vector2 normalized)
        {
            var rect = _rectTransform.rect;
            var localPos = new Vector2(
                Mathf.Lerp(rect.xMin, rect.xMax, normalized.x),
                Mathf.Lerp(rect.yMin, rect.yMax, normalized.y)
            );
            return _rectTransform.TransformPoint(localPos);
        }
    }
}