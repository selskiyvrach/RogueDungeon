using UnityEngine;

namespace RogueDungeon.Enemies
{
    public class EnemyParents : MonoBehaviour
    {
        [field: SerializeField] public Transform MiddleParent {get; private set; }
        [field: SerializeField] public Transform LeftParent {get; private set; }
        [field: SerializeField] public Transform RightParent {get; private set; }
    }
}