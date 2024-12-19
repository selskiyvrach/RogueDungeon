namespace RogueDungeon.Items.Bahaviour.Unsheather
{
    public class UnsheathDuration : IUnsheathDuration
    {
        public float Value { get; }

        public UnsheathDuration(float value) => 
            Value = value;
    }
}