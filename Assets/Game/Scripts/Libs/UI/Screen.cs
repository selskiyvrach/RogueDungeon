using UnityEngine;

namespace Libs.UI
{
    public abstract class Screen : MonoBehaviour
    {
        public abstract int SortingOrder { get; }
    }
}