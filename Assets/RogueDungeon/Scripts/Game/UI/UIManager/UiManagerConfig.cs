using UnityEngine;

namespace Game.UI.UIManager
{
    public class UiManagerConfig : ScriptableObject
    {
        [field: SerializeField] public UI.Screens.Screen[] Screens { get; private set; }
    }
}