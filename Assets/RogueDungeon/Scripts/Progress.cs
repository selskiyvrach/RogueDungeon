using UnityEngine.Assertions;

namespace RogueDungeon
{
    public readonly struct Progress
    {
        private readonly float _value;

        public Progress(float value)
        {
            Assert.IsTrue(value is < 1 and > 0);
            _value = value;
        }

        public static implicit operator float(Progress progress) => progress._value;
        public static implicit operator Progress(float progress) => new(progress);
    }
}