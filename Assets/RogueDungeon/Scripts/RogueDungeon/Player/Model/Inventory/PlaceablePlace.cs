using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class PlaceablePlace : MonoBehaviour
    {
        public virtual Vector3 GetCuredPosition(Vector3 intendedPosition) => 
            intendedPosition;
    }
}