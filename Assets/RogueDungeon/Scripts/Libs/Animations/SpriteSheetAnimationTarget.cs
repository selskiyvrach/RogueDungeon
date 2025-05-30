using UnityEngine;

namespace Libs.Animations
{
    public class SpriteSheetAnimationTarget : MonoBehaviour, ISpriteSheetAnimationTarget
    {
        [field: SerializeField]public SpriteRenderer SpriteRenderer { get; private set; }
    }
}