using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.WFC
{
    [CreateAssetMenu(menuName = "Configs/WFC/Tile", fileName = "Tile", order = 0)]
    public class Tile : ScriptableObject
    {
        [field: PreviewField, SerializeField] public Sprite Sprite { get; private set; }
        [field:SerializeField] public bool Up {get; private set;}
        [field:SerializeField] public bool Right {get; private set;}
        [field:SerializeField] public bool Down {get; private set;}
        [field:SerializeField] public bool Left {get; private set;}
    }
}