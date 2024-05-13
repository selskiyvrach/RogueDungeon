using System.Collections.Generic;
using System.Linq;
using RogueDungeon.Utils;

namespace RogueDungeon.WFC
{
    public class WFC
    {
        private int _sizeX = 10;
        private int _sizeY = 10;
        private Cell[,] _grid;
        
        private Tile[] _tileOptions;
        private Cell _borderCell;

        private readonly Stack<Cell> _cellsToProcess = new ();
        private readonly HashSet<Cell> _processedCells = new ();

        public Cell[,] CreateGrid(IEnumerable<Tile> tileOptions, int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            
            _tileOptions = tileOptions.ToArray();
            _borderCell = new Cell(-1, -1, _tileOptions.Where(n => n.Empty()).ToArray());

            _grid = new Cell[_sizeX, _sizeY];
            for (var y = 0; y < _sizeY; y++)
            for (var x = 0; x < _sizeX; x++)
                _grid[x, y] = new Cell(x, y,  _tileOptions);

            var startingCell = _grid[_sizeX / 2, _sizeY / 2]; 
            startingCell.Options.Clear();            
            startingCell.Options.AddRange(_tileOptions.Where(n => n.IsHorizontalCorridor()));
            CollapseCell(startingCell);
            return _grid;
        }

        void CollapseCell(Cell cell)
        {
            var tile = cell.Options.Random();
            cell.Options.Clear();
            cell.Options.Add(tile);
            cell.Collapsed = true;
            _cellsToProcess.Push(cell);
            while (_cellsToProcess.Any()) 
                ProcessNeighbors(_cellsToProcess.Pop());

        }

        public void NextIteration()
        {
            Cell minCell = null;
            foreach (var cell in _grid)
            {
                if(cell.Collapsed)
                    continue;

                if (minCell == null || cell.Options.Count < minCell.Options.Count)
                    minCell = cell;
            }
            CollapseCell(minCell);
        }

        private void ProcessNeighbors(Cell cell)
        {
            foreach (var (edge, neighbour) in Neighbours(cell))
            {
                if(neighbour.Collapsed)
                    continue;
                var oldOptions = new List<Tile>(neighbour.Options);
                var validOptions = oldOptions.Where(n => cell.Options.Any(m => m.Matches(n, edge)));
                neighbour.Options.Clear();
                neighbour.Options.AddRange(validOptions);
                if(oldOptions.Count > neighbour.Options.Count)
                    _cellsToProcess.Push(neighbour);
            }
        }

        private IEnumerable<(Edge edge, Cell cell)> Neighbours(Cell cell)
        {
            if (cell.X < _sizeX - 1)
                yield return (Edge.Right, _grid[cell.X + 1, cell.Y]);
            if (cell.X > 0)
                yield return (Edge.Left, _grid[cell.X - 1, cell.Y]);
            if (cell.Y < _sizeY - 1)
                yield return (Edge.Up, _grid[cell.X, cell.Y + 1]);
            if (cell.Y > 0)
                yield return (Edge.Down, _grid[cell.X, cell.Y - 1]);
        }
    }
}