namespace Common.Keys
{
    public struct Key
    {
        public static readonly Key NONE = new(NONE_NAME);
        public const string NONE_NAME = "None";
        
        private readonly string _name;
        private readonly int _value;

        public Key(string name)
        {
            _name = name;
            _value = name.GetHashCode();
        }
        
        public override bool Equals(object obj) => 
            obj is Key other && other._value == _value;

        public override int GetHashCode() => _value.GetHashCode();
        public static bool operator ==(Key left, Key right) => left._value == right._value;
        public static bool operator !=(Key left, Key right) => left._value != right._value;
        public override string ToString() => $"Key(Name: {_name}, Value: {_value})";
    }
}