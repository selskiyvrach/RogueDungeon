using UnityEngine;

namespace Game.Features.Combat.Domain
{
    public class BattleField : MonoBehaviour
    {
        public Vector2Int Position { set => transform.position = new Vector3(value.x, 0, value.y); }
        public Vector2Int Direction { set => transform.forward = new Vector3(value.x, 0, value.y); }
    }
}