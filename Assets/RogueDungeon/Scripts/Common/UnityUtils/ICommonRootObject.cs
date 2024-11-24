using UnityEngine;

namespace Common.UnityUtils
{
    public interface ICommonRootObject
    {
        Transform CommonRootTransform { get; }
    }
}