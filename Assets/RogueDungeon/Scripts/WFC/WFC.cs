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

        public List<Cell> CreateGrid(IEnumerable<Tile> tileOptions)
        {
            _tileOptions = tileOptions.ToArray();
            
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
            for (var x = 0; x < _sizeX; x++)
            for (var y = 0; y < _sizeY; y++)
            {
                var i = x + y * _sizeX;
                if (_grid[i].Collapsed)
                    continue;

                var options = new List<Tile>(_tileOptions);
                // check up
                if (y > 0) 
                    ApplyNeighbour(_grid[x + (y - 1) * _sizeX], options, Edge.Up);

                // check down
                if (y < _sizeY - 1) 
                    ApplyNeighbour(_grid[x + (y + 1) * _sizeX], options, Edge.Down);

                // check right
                if (x < _sizeX - 1) 
                    ApplyNeighbour(_grid[x + 1 + y * _sizeX], options, Edge.Left);

                // check left
                if (x > 0) 
                    ApplyNeighbour(_grid[x - 1 + y * _sizeX], options, Edge.Right);

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