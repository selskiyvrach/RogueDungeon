using System.Collections.Generic;

namespace RogueDungeon.WFC
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool Collapsed => Options.Count == 1;
        public List<Tile> Options { get; }

        public Cell(int x, int y, IEnumerable<Tile> options)
        {
            X = x;
            Y = y;
            Options = new List<Tile>(options);
        }
    }

    public class WFC
    {
        private readonly int _sizeX = 10;
        private readonly int _sizeY = 10;
        
        private readonly List<Cell> _cells = new();
        
        private Tile[] _tiles;
        private Cell _currentCell;
        
        private int _iterations;

        public void Run()
        {
            InitCells();
        }

        private void InitCells()
        {
            for (var x = 0; x < _sizeX; x++)
            for (var y = 0; y < _sizeY; y++)
                _cells.Add(new Cell(x, y, _tiles));
        }
    }
}