using UnityEngine;

namespace Game.Libs.Items
{
    public interface IItem
    {
        string Id { get; }
        string TypeId { get; }
        public Vector2Int Size { get; }
    }
}