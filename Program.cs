using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(20,20);
            grid.GetCell(grid.size.Width / 2-1, grid.size.Height / 2-1).CellState = Cell.CellStates.Wall;
            Console.WriteLine(grid.GetRepresented());
            Console.ReadKey();
        }
    }
}
