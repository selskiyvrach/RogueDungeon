using RogueDungeon.Levels;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Map
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private MapDisplayConfig _config;
        [SerializeField] private SpriteRenderer _tilePrefab;
        
        private Level _level;

        [Inject]
        public void Construct(Level level)
        {
            _level = level;
            _level.OnChangedRoom += RepaintMap;
            RepaintMap();
        }

        private void RepaintMap()
        {
            foreach (var room in _level.Rooms)
            {
                
            }
        }
    }
}