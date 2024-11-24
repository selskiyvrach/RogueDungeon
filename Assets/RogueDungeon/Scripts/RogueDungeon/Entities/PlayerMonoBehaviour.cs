using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Entities
{
    public class PlayerMonoBehaviour : MonoBehaviour
    {
        [field: SerializeField] public CharacterAnimationRoot AnimationRoot { get; private set; }
        [field: SerializeField] public Transform WeaponParent { get; private set; }
        [field: SerializeField] public Transform CameraParent { get; private set; }
    }
}