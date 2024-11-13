using UnityEngine;

namespace RogueDungeon.Gameplay
{
    public class WeaponParentReference : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        public Transform Parent => _parent;
    }
}