using System;
using System.Collections.Generic;
using System.Linq;
using Levels;
using RogueDungeon.Items.View;
using UnityEngine;

namespace RogueDungeon.Scripts.RogueDungeon.Player.Presenter
{
    public class HandheldMapPresenter : IMapInfoProvider
    {
        private readonly Level _level;
        private readonly HandheldMapView _mapView;
        Vector2Int IMapInfoProvider.CurrentPlayerPosition => _level.CurrentRoom.Coordinates;

        event Action IMapInfoProvider.OnPlayerPositionChanged
        {
            add => _level.OnChangedRoom += value;
            remove => _level.OnChangedRoom -= value;
        }

        IEnumerable<TileInfo> IMapInfoProvider.GetTiles() => 
            _level.Rooms.Select(n => new TileInfo(
                hasUp: n.AdjacentRooms.HasAdjacentRoom(Vector2Int.up), 
                hasDown: n.AdjacentRooms.HasAdjacentRoom(Vector2Int.down), 
                hasLeft: n.AdjacentRooms.HasAdjacentRoom(Vector2Int.left), 
                hasRight: n.AdjacentRooms.HasAdjacentRoom(Vector2Int.right), 
                n.Coordinates));

        public HandheldMapPresenter(Level level, HandheldMapView mapView)
        {
            _level = level;
            _mapView = mapView; 
        }

        public void Show() => 
            _mapView.Show(this);

        public void Hide() => 
            _mapView.Hide();
    }
}