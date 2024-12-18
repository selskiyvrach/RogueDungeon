namespace RogueDungeon.Player
{
    public readonly struct Attribute
    {
        public readonly int Value;
        public readonly float AttributeBonus;

        public Attribute(int value)
        {
            Value = value;
            AttributeBonus = ((float)Value - 5) / 5;
        }
    }
}