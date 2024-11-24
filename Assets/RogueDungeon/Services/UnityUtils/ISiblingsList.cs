using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Services.UnityUtils
{
    public interface ISiblingsList<out T> : IEnumerable<T> where T : Component
    {
        int Count { get; }
        T this[int index] { get; }
        void SetActiveItemsCount(int count);
    }
}