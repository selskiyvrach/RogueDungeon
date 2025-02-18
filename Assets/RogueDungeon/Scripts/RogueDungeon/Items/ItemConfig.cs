using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public MoveSetConfig MoveSetConfig { get; private set; }
    }
}