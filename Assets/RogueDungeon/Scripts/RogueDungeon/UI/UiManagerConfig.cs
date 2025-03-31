using UnityEngine;

namespace RogueDungeon.UI
{
    public class UiManagerConfig : ScriptableObject
    {
        [field: SerializeField] public Screen[] Screens { get; private set; }
    }
}