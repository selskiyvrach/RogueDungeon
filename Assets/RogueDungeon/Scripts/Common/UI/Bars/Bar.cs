using UnityEngine;

namespace Common.UI.Bars
{
    public abstract class Bar : MonoBehaviour, IBar
    {
        public abstract float Value { get; set; }
    }
}