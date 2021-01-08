using System;
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
            Grid grid = new Grid(100, 40);
            Console.WriteLine(grid.GetRepresented());
            Console.ReadKey();
        }
    }
}
