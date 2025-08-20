using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Domain;
using Game.Libs.UI;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    
    
    
    public abstract class Container : HoverableGraphic, IContainerView
    {
        protected RectTransform _rectTransform;
        public string Id { get; }

        protected abstract float CellSize { get; }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            _rectTransform = GetComponent<RectTransform>().ThrowIfNull();
        }

        public void AcceptVisitor(IContainerVisitor visitor)
        {
        }
    }
}