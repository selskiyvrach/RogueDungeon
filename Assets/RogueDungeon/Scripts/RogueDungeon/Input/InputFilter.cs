using UnityEngine;

namespace RogueDungeon.Input
{
    public class InputFilter : ScriptableObject
    {
        [field: SerializeField] public InputKey[] AllowedKeys { get; private set; }
    }
}