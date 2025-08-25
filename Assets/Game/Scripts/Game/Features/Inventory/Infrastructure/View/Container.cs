using System.Linq;
using Game.Features.Inventory.App.Presenters;
using Game.Libs.UI;
using Libs.Utils.DotNet;
using Libs.Utils.Unity;
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
            item.SetParent(_rectTransform);
            item.SetCellSize(CellSize);
            item.SetLocalPosition((posNormalized - Vector2.one * .5f) * _rectTransform.sizeDelta);
        }

        public IItemView RemoveItem(string id)
        {
            var item = _rectTransform.GetDirectChildren<IItemView>().First(n => n.Id == id);
            item.SetParent(null);
            return item;
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

        public void Reset()
        {
            for (var i = _rectTransform.childCount - 1; i >= 0; i--) 
                Destroy(_rectTransform.GetChild(i).gameObject);
        }
    }
}