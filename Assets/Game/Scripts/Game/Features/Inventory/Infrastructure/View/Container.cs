using Game.Features.Inventory.App.Presenters;
using Game.Libs.UI;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public abstract class Container : HoverableGraphic, IContainerView
    {
        private RectTransform _rectTransform;
        public string Id { get; }
        protected abstract float CellSize { get; }

        protected override void OnValidate()
        {
            base.OnValidate();
            _rectTransform = GetComponent<RectTransform>().ThrowIfNull();
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