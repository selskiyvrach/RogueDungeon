using UnityEngine;
using Zenject;

namespace RogueDungeon.Items
{
    public class HandheldItem : MonoBehaviour, IHandheldItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private IItemManipulationDurationCalculator _durationCalculator;

        public IItem Item { get; private set; }
        public float SheathDuration => _durationCalculator.Calculate(Item);
        public float UnsheathDuration => _durationCalculator.Calculate(Item);

        [Inject]
        public void Construct(IItemManipulationDurationCalculator manipulationDurationCalculator) => 
            _durationCalculator = manipulationDurationCalculator;

        public void Setup(IItem item)
        {
            Item = item;
            _spriteRenderer.sprite = Item?.Sprite;
        }

        public void SetVisible(bool value) => 
            gameObject.SetActive(value);

        public void SetEnabled(bool value)
        {
            if(value)
                Item.Enable();
            else
                Item.Disable();
        }
    }
}