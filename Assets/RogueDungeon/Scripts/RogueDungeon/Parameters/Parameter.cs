namespace RogueDungeon.Parameters
{
    public class Parameter : IParameter
    {
        public float Value { get; }

        public Parameter(float value) => 
            Value = value;
    }
}