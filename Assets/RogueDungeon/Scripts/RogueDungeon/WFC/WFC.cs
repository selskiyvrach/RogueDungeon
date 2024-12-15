using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using UnityEngine;

namespace RogueDungeon.WFC
{
    public class WFC
    {
        private int _sizeX = 10;
        private int _sizeY = 10;
        private Cell[,] _grid;
        
        private Tile[] _tileOptions;

        private readonly Stack<Cell> _cellsToProcess = new ();
        private Cell _borderCell;
        private Vector2Int _startingCell => new(_sizeX / 2, _sizeY / 2); 

        public Cell[,] CreateGrid(IEnumerable<Tile> tileOptions, int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _sizeY = sizeY;
            
            _tileOptions = tileOptions.ToArray();

            var gotValidGrid = false;
            while (!gotValidGrid)
            {
                CreateInternal();
                gotValidGrid = IsGridValid();
            }
            
            return _grid;
        }

        /// <summary>
        /// If the walkable area is not too small and fully traversable
        /// </summary>
        public bool IsGridValid()
        {
            var walkableCount = _grid.Cast<Cell>().Count(cell => !cell.TileOptions[0].IsEmpty());
            var cellsExceptBorders = _sizeX * _sizeY - _sizeX * 2 - _sizeY * 2 + 4; 
            if (walkableCount < cellsExceptBorders * 0.75)
                return false;

            var startingCell = _grid[_startingCell.x, _startingCell.y];
            var cells = new Stack<Cell>();
            var connectedCells = new HashSet<Cell>();
            cells.Push(startingCell);
            connectedCells.Add(startingCell);
            while (cells.Any())
            {
                var currCell = cells.Pop();
                foreach (var (edge, neighbour) in Neighbours(currCell))
                {
                    if(neighbour.TileOptions[0].IsEmpty())
                        continue;
                    if(!currCell.TileOptions[0].ConnectedTo(neighbour.TileOptions[0], edge))
                        continue;
                    if(!connectedCells.Add(neighbour))
                        continue;
                    cells.Push(neighbour);
                }
            }

            return walkableCount == connectedCells.Count;
        }

        private void CreateInternal()
        {
            _grid = new Cell[_sizeX, _sizeY];
            for (var y = 0; y < _sizeY; y++)
            for (var x = 0; x < _sizeX; x++)
                _grid[x, y] = new Cell(x, y,
                    x == 0 || x == _sizeX - 1 || y == 0 || y == _sizeY - 1 ? _tileOptions.Where(n => n.IsEmpty()) : _tileOptions);

            var startingCell = _grid[_startingCell.x, _startingCell.y];
            startingCell.TileOptions.Clear();
            startingCell.TileOptions.AddRange(_tileOptions.Where(n => n.IsVerticalCorridor()));
            CollapseCell(startingCell);
        }

        void CollapseCell(Cell cell)
        {
            var tile = cell.TileOptions.Random();
            cell.TileOptions.Clear();
            cell.TileOptions.Add(tile);
            cell.Collapsed = true;
            _cellsToProcess.Push(cell);
            while (_cellsToProcess.Any()) 
                ProcessNeighbors(_cellsToProcess.Pop());
            NextIteration();
        }

        private void NextIteration()
        {
            Cell minCell = null;
            foreach (var cell in _grid)
            {
                if(cell.Collapsed)
                    continue;

                if (minCell == null || cell.TileOptions.Count < minCell.TileOptions.Count)
                    minCell = cell;
            }
            if(minCell != null)
                CollapseCell(minCell);
        }

        private void ProcessNeighbors(Cell cell)
        {
            foreach (var (edge, neighbour) in Neighbours(cell))
            {
                if(neighbour.Collapsed)
                    continue;
                var oldOptions = new List<Tile>(neighbour.TileOptions);
                var validOptions = oldOptions.Where(n => cell.TileOptions.Any(m => m.Matches(n, edge)));
                neighbour.TileOptions.Clear();
                neighbour.TileOptions.AddRange(validOptions);
                if(oldOptions.Count > neighbour.TileOptions.Count)
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