using UnityEngine;

namespace Common.Animations
{
    public class SpriteSheetAnimationTarget : MonoBehaviour, ISpriteSheetAnimationTarget
    {
        [field: SerializeField]public SpriteRenderer SpriteRenderer { get; private set; }
    }
}