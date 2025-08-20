using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class SlotContainer : Container
    {
        [SerializeField] private float _cellSize = 40;
        protected override float CellSize => _cellSize;
    }
}