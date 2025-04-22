using System;
using System.Collections.Generic;
using RogueDungeon.Items;
using RogueDungeon.Levels;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Map
{
    public class HandheldMapPresenter : HandHeldItemPresenter
    {
        private readonly List<SpriteRenderer> _tiles = new();
        
        [SerializeField] private MapDisplayConfig _config;
        [SerializeField] private Transform _mapElementsParent;
        
        private SpriteRenderer _playerMarker;
        private Level _level;

        [Inject]
        public void Construct(Level level)
        {
            _level = level;
            _level.OnChangedRoom += RepaintMap;
            _playerMarker = Instantiate(_config.PlayerMarkerPrefab, _mapElementsParent);
            RepaintMap();
        }

        private void RepaintMap()
        {
            var i = 0;
            foreach (var room in _level.Rooms)
            {
                if(i == _tiles.Count)
                    _tiles.Add(Instantiate(_config.TilePrefab, _mapElementsParent));
                SetupTile(_tiles[i], room);
                _tiles[i].sortingOrder = 1;
                i++;
            }
            _playerMarker.transform.localPosition = new Vector3(_level.CurrentRoom.Coordinates.x, _level.CurrentRoom.Coordinates.y) * _config.TileSpacing;
            _playerMarker.sortingOrder = 2;

            CenterMap();
        }

        private void SetupTile(SpriteRenderer tile, IRoom room)
        {
            tile.transform.localPosition = new Vector3(room.Coordinates.x, room.Coordinates.y) * _config.TileSpacing;
                
            var hasUp = room.AdjacentRooms.HasAdjacentRoom(Vector2Int.up);
            var hasDown = room.AdjacentRooms.HasAdjacentRoom(Vector2Int.down);
            var hasLeft = room.AdjacentRooms.HasAdjacentRoom(Vector2Int.left);
            var hasRight = room.AdjacentRooms.HasAdjacentRoom(Vector2Int.right);

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