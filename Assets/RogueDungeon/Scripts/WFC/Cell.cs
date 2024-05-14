using System.Collections.Generic;

namespace RogueDungeon.WFC
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool Collapsed { get; set; }
        public List<Tile> TileOptions { get; }

        public Cell(int x, int y, IEnumerable<Tile> options)
        {
            X = x;
            Y = y;
            TileOptions = new List<Tile>(options);
        }
    }
}