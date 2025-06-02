using UnityEngine;
using Screen = Game.UI.Screens.Screen;

namespace Game.UI.UIManager
{
    public class UiManagerConfig : ScriptableObject
    {
        [field: SerializeField] public Screen[] Screens { get; private set; }
    }
}