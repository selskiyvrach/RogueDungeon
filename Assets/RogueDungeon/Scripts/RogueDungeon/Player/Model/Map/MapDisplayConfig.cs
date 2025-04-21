using UnityEngine;

namespace RogueDungeon.Map
{
    public class MapDisplayConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Corridor { get; private set; }
        [field: SerializeField] public Sprite XJoint { get; private set; }
        [field: SerializeField] public Sprite TJoint { get; private set; }
        [field: SerializeField] public Sprite LJoint { get; private set; }
        [field: SerializeField] public Sprite PlayerMarker { get; private set; } 
    }
}