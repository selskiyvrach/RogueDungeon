using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class GridContainerView : ContainerView
    {
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        public override float CellSize => _gridLayoutGroup.cellSize.x;
    }
}