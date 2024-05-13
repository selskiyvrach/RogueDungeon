using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Utils;

namespace RogueDungeon.WFC
{
    public class WFC
    {
        private readonly int _sizeX = 10;
        private readonly int _sizeY = 10;
        
        private List<Cell> _grid = new();
        private Tile[] _tileOptions;
        private Cell _currentCell;

        public List<Cell> CreateGrid(IEnumerable<Tile> tileOptions)
        {
            _tileOptions = tileOptions.ToArray();
            InitGrid();
            var size = _sizeX * _sizeY;
            for (var i = 0; i < size; i++)
                CheckEntropy();
            return _grid;
        }

        void CheckEntropy()
        {
            var tempGrid = _grid.Where(n => !n.Collapsed).ToList();
            tempGrid.Sort((a, b) => a.Options.Count - b.Options.Count);
            tempGrid.RemoveAll(n => n.Options.Count > tempGrid[0].Options.Count);
            CollapseCell(tempGrid);
        }

        void CollapseCell(List<Cell> tempGrid)
        {
            var cell = tempGrid.Random();
            var tile = cell.Options.Random();
            cell.Options.Clear();
            cell.Options.Add(tile);
            UpdateGeneration();
        }

        void UpdateGeneration()
        {
            var newGrid = new List<Cell>(_grid);
            for (var x = 0; x < _sizeX; x++)
            for (var y = 0; y < _sizeY; y++)
            {
                var i = x + y * _sizeX;
                if (_grid[i].Collapsed)
                    continue;

                var options = new List<Tile>(_tileOptions);
                // check up
                if (y > 0)
                {
                    var up = _grid[x + (y - 1) * _sizeX];
                    ApplyNeighbour(up, options, Edge.Up);
                }
                
                // check down
                if (y < _sizeY - 1)
                {
                    var down = _grid[x + (y + 1) * _sizeX];
                    ApplyNeighbour(down, options, Edge.Down);
                }
                
                // check right
                if (x < _sizeX - 1)
                {
                    var right = _grid[x + 1 + y * _sizeX];
                    ApplyNeighbour(right, options, Edge.Left);
                }
                
                // check left
                if (x > 0)
                {
                    var left = _grid[x - 1 + y * _sizeX];
                    ApplyNeighbour(left, options, Edge.Right);
                }
                
                newGrid[i].Options.Clear();
                newGrid[i].Options.AddRange(options);
            }

            _grid = newGrid;
        }

        private void ApplyNeighbour(Cell neighbour, List<Tile> options, Edge edge)
        {
            var valid = new List<Tile>();
            foreach (var option in neighbour.Options)
                valid.AddRange(_tileOptions.Where(n => option.Matches(n, edge)));

            RemoveInvalidOptions(options, valid);
        }

        void RemoveInvalidOptions(List<Tile> options, IEnumerable<Tile> validOptions)
            => options.RemoveAll(n => !validOptions.Contains(n));

        void InitGrid()
        {
            for (var x = 0; x < _sizeX; x++)
            for (var y = 0; y < _sizeY; y++)
                _grid.Add(new Cell(x, y, _tileOptions));
        }
    }
}