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
            Grid grid = new Grid(30,20);
            grid.GetCell(5, 10).CellState = Cell.CellStates.Wall;
            grid.SetStartPos(new Point(0, 10));
            grid.SetTargetPos(new Point(29, 10));
            /*for (int Y = 0; Y < grid.size.Height; Y++)
                if (Y != 4)
                    grid.GetCell(15, Y).CellState = Cell.CellStates.Wall;*/
            Console.WriteLine(grid.GetRepresented() + "\n\n\n");
            Visualization visualization = Algorithms.AStar(grid);
            Console.WriteLine(visualization.GetGridString());
            Console.ReadKey();
        }
    }
}
