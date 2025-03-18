using Common.MoveSets;
using UnityEngine;

namespace RogueDungeon.Items
{
    public interface IItem
    {
        Sprite Sprite { get; }
        MoveSetConfig MoveSetConfig { get; }
    }

    public class Item : IItem
    {
        private readonly ItemConfig _config;

        public Sprite Sprite => _config.Sprite;
        public MoveSetConfig MoveSetConfig => _config.MoveSetConfig;

        protected Item(ItemConfig config) => 
            _config = config;
    }
}