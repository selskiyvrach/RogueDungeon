using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Utils;

namespace RogueDungeon.WFC
{
    public class WFC
    {
        private readonly int _sizeX = 10;
        private readonly int _sizeY = 10;
        private readonly List<Cell> _grid = new();
        
        private Tile[] _tileOptions;
        private Cell _borderCell;

        public List<Cell> CreateGrid(IEnumerable<Tile> tileOptions)
        {
            _tileOptions = tileOptions.ToArray();
            _borderCell = new Cell(-1, -1, _tileOptions.Where(n => n.Empty()).ToArray());
            
            for (var y = 0; y < _sizeY; y++)
            for (var x = 0; x < _sizeX; x++)
                _grid.Add(new Cell(x, y, _tileOptions));;
            
            var size = _sizeX * _sizeY;
            for (var i = 0; i < size; i++)
                CheckEntropy();
            
            return _grid;
        }

        void CheckEntropy()
        {
            var collapseCandidates = _grid.Where(n => !n.Collapsed).ToList();
            collapseCandidates.Sort((a, b) => a.Options.Count - b.Options.Count);
            collapseCandidates.RemoveAll(n => n.Options.Count > collapseCandidates[0].Options.Count);
            if(collapseCandidates.Count == 0)
                return;
            CollapseCell(collapseCandidates.Random());
        }

        void CollapseCell(Cell cell)
        {
            var tile = cell.Options.Random();
            cell.Options.Clear();
            cell.Options.Add(tile);
            UpdateGeneration();
        }

        void UpdateGeneration()
        {
            for (var x = 0; x < _sizeX; x++)
            for (var y = 0; y < _sizeY; y++)
            {
                var i = x + y * _sizeX;
                if (_grid[i].Collapsed)
                    continue;

                var options = new List<Tile>(_tileOptions);
                // check up
                ApplyNeighbour(y > 0 ? _grid[x + (y - 1) * _sizeX] : _borderCell, options, Edge.Up);

                // check down
                ApplyNeighbour(y < _sizeY - 1 ? _grid[x + (y + 1) * _sizeX] : _borderCell, options, Edge.Down);

                // check right
                ApplyNeighbour(x < _sizeX - 1 ? _grid[x + 1 + y * _sizeX] : _borderCell, options, Edge.Left);

                // check left
                ApplyNeighbour(x > 0 ? _grid[x - 1 + y * _sizeX] : _borderCell, options, Edge.Right);

                _grid[i].Options.Clear();
                _grid[i].Options.AddRange(options);
            }
        }

        private void ApplyNeighbour(Cell neighbour, List<Tile> options, Edge edge)
        {
            var valid = new List<Tile>();
            foreach (var option in neighbour.Options)
                valid.AddRange(_tileOptions.Where(n => option.Matches(n, edge)));

            options.RemoveAll(n => !valid.Contains(n));
        }
    }
}