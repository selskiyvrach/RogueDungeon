using System;
using Game.Features.Inventory.App.Presenters;
using Game.Libs.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public abstract class ContainerView : HoverableGraphic, IContainerView
    {
        [SerializeField, ReadOnly] private RectTransform _rectTransform;
        [field: SerializeField] public virtual float CellSize { get; private set; } = 40;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _rectTransform ??= GetComponent<RectTransform>() ?? throw new Exception();
        }

        public void PlaceItem(IItemView item, Vector2 posNormalized)
        {
            var rt = _rectTransform;
            var rect = rt.rect;

            var localPos = new Vector3((posNormalized.x - rt.pivot.x) * rect.width, (posNormalized.y - rt.pivot.y) * rect.height, 0f);
            var worldPos = rt.TransformPoint(localPos);
            
            item.SetCellSize(CellSize);
            item.SetPosition(worldPos);
            item.ProjectionView.SetPosition(worldPos);
        }

        public Vector2 ScreenPosToLocalPosNormalized(Vector2 point, Camera cam)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, point, cam, out var position);
            var result = (position + _rectTransform.sizeDelta / 2) / _rectTransform.sizeDelta;
            return result;
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