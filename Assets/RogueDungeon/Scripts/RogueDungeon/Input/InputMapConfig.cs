using UnityEngine;

namespace RogueDungeon.Input
{
    public class InputMapConfig : ScriptableObject
    {
        [field: SerializeField] public InputUnit[] Units { get; private set; }
    }
}