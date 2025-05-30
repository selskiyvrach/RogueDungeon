using System.Collections.Generic;
using UnityEngine;

namespace Libs.UI
{
    public interface ISiblingsList<out T> : IEnumerable<T> where T : Component
    {
        int Count { get; }
        T this[int index] { get; }
        void SetActiveItemsCount(int count);
    }
}