using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class PlaceablePlace : MonoBehaviour
    {
        public virtual Vector3 GetCuredPosition(Vector3 intendedPosition, out bool canBePlaced)
        {
            canBePlaced = true;
            return intendedPosition;
        }
    }
}