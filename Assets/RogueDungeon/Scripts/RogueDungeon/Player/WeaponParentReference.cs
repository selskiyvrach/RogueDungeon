using UnityEngine;

namespace RogueDungeon.Player
{
    public class WeaponParentReference : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        public Transform Parent => _parent;
    }
}