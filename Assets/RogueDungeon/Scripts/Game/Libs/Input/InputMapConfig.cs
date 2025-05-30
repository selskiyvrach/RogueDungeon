using UnityEngine;

namespace Game.Libs.Input
{
    public class InputMapConfig : ScriptableObject
    {
        [field: SerializeField] public InputUnit[] Units { get; private set; }
    }
}