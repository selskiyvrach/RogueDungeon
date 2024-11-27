using System;

namespace Common.FSM
{
    public readonly struct EnumComparer<T> : IComparable<EnumComparer<T>>, IComparable<T> where T : Enum 
    {
        public readonly T Item;

        public EnumComparer(T item) => 
            Item = item;
        
        public int CompareTo(EnumComparer<T> other) => 
            Convert.ToInt32(Item).CompareTo(Convert.ToInt32((object)other.Item));
        
        public int CompareTo(T other) => 
            Convert.ToInt32(Item).CompareTo(Convert.ToInt32(other));
    }
}