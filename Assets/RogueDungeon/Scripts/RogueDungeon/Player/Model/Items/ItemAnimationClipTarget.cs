using UnityEngine;

namespace RogueDungeon.Items
{
    public class ItemAnimationClipTarget : MonoBehaviour
    {
        [field: SerializeField] public bool IsRightHand { get; private set; }
    }
}