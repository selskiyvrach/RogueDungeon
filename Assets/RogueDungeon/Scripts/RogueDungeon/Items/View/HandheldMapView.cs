using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Items.View
{
    public class HandheldMapView : HandHeldItemView
    {
        private readonly List<SpriteRenderer> _tiles = new();
        
        [SerializeField] private MapViewConfig _config;
        [SerializeField] private Transform _mapElementsParent;
        
        private SpriteRenderer _playerMarker;
        private IMapInfoProvider _map;

        public void Show(IMapInfoProvider mapInfoProvider)
        {
            gameObject.SetActive(true);
            _map = mapInfoProvider;
            _map.OnPlayerPositionChanged += RepaintMap;
            _playerMarker = Instantiate(_config.PlayerMarkerPrefab, _mapElementsParent);
            RepaintMap();
        }

        public void Hide() => 
            gameObject.SetActive(false);

        private void RepaintMap()
        {
            var i = 0;
            foreach (var tile in _map.GetTiles())
            {
                if(i == _tiles.Count)
                    _tiles.Add(Instantiate(_config.TilePrefab, _mapElementsParent));
                SetupTile(_tiles[i], tile);
                _tiles[i].sortingOrder = 1;
                i++;
            }
            _playerMarker.transform.localPosition = new Vector3(_map.CurrentPlayerPosition.x, _map.CurrentPlayerPosition.y) * _config.TileSpacing;
            _playerMarker.sortingOrder = 2;

            CenterMap();
        }

        private void SetupTile(SpriteRenderer tile, TileInfo room)
        {
            tile.transform.localPosition = new Vector3(room.Position.x, room.Position.y) * _config.TileSpacing;
                
            var hasUp = room.HasUp;
            var hasDown = room.HasDown;
            var hasLeft = room.HasLeft;
            var hasRight = room.HasRight;

            var connections = 0;
                
            if (hasUp) connections++;
            if (hasDown) connections++;
            if (hasLeft) connections++;
            if (hasRight) connections++;

            tile.transform.localRotation = Quaternion.identity;

            if (connections == 4)
            {
                tile.sprite = _config.XJoint;
            }
            else if (connections == 3)
            {
                tile.sprite = _config.TJoint;
                RotateToTJoint();
            }
            else if (connections == 2)
            {
                if ((hasUp && hasDown) || (hasLeft && hasRight))
                {
                    tile.sprite = _config.Corridor;
                    RotateToCorridor();
                }
                else
                {
                    tile.sprite = _config.LJoint;
                    RotateToLJoint();
                }
            }
            else
            {
                tile.sprite = _config.Corridor;
                RotateToCorridor();
            }

            void RotateToTJoint()
            {
                if (!hasUp)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 0);
                else if (!hasRight)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 90);
                else if (!hasDown)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 180);
                else if (!hasLeft)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 270);
            }

            void RotateToCorridor()
            {
                if (hasLeft || hasRight)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }

            void RotateToLJoint()
            {
                if (hasUp && hasRight)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 0);
                else if (hasRight && hasDown)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 270);
                else if (hasDown && hasLeft)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 180);
                else if (hasLeft && hasUp)
                    tile.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
        }

        private void CenterMap()
        {
            var topExtent = 0f;
            var bottomExtent = 0f;
            var leftExtent = 0f;
            var rightExtent = 0f;
            
            foreach (var tile in _tiles)
            {
                var position = tile.transform.localPosition;
                
                if(position.y > topExtent)
                    topExtent = position.y;
                if(position.y < bottomExtent)
                    bottomExtent = position.y;
                if(position.x > rightExtent)
                    rightExtent = position.x;
                if(position.x < leftExtent)
                    leftExtent = position.x;
            }
            
            var offset = new Vector3(- (rightExtent + leftExtent) / 2,- (topExtent + bottomExtent) / 2, 0);
            _mapElementsParent.localPosition = offset;
        }
    }
}