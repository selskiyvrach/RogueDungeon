using UnityEngine;

namespace Common.UI
{
    public abstract class Screen : MonoBehaviour
    {
        public abstract int SortingOrder { get; }
    }
}