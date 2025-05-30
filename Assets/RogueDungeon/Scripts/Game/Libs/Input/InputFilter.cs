using UnityEngine;

namespace Game.Libs.Input
{
    public class InputFilter : ScriptableObject
    {
        [field: SerializeField] public InputKey[] AllowedKeys { get; private set; }
    }
}