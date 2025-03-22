using UnityEngine;

namespace Common.UI
{
    public class UiManagerConfig : ScriptableObject
    {
        [field: SerializeField] public Screen[] Screens { get; private set; }
    }
}