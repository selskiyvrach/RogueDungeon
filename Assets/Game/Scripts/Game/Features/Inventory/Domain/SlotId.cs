using System;
using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public enum Hand
    {
        None, 
        Right, 
        Left
    }

    public readonly struct SlotId : IEquatable<SlotId>
    {
        private readonly int _index;
        private readonly SlotType _slotType;
        private readonly Hand _hand;

        public SlotId(SlotType slotType, Hand hand, int index)
        {
            _index = index;
            _hand = hand;
            _slotType = slotType;
        }

        public static readonly SlotId HANDHELD_R_0 = Handheld(Hand.Right, 0);
        public static readonly SlotId HANDHELD_R_1 = Handheld(Hand.Right, 1);
        public static readonly SlotId HANDHELD_R_2 = Handheld(Hand.Right, 2);

        public static readonly SlotId HANDHELD_L_0 = Handheld(Hand.Left, 0);
        public static readonly SlotId HANDHELD_L_1 = Handheld(Hand.Left, 1);
        public static readonly SlotId HANDHELD_L_2 = Handheld(Hand.Left, 2);

        public static SlotId Handheld(Hand hand, int index) => new(SlotType.Handheld, hand, index);

        public bool Equals(SlotId other) => 
            _index == other._index && _slotType == other._slotType && _hand == other._hand;

        public override bool Equals(object obj) => 
            obj is SlotId other && Equals(other);

        public override int GetHashCode() => 
            HashCode.Combine(_index, (int)_slotType, (int)_hand);
    }
}