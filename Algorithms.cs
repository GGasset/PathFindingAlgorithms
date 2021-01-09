using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingAlgorithms
{
    class Algorithms
    {
        Visualization AStar(Grid grid)
        {
            Stack<Point> path = new Stack<Point>();
            Queue<Cell> checkedCells = new Queue<Cell>();
            Point currentPosition = grid.startingPoint, lastPosition = grid.startingPoint;

            do //Calculate until target is reached
            {

            } while (currentPosition != grid.targetPoint);

            while (currentPosition != grid.startingPoint) //Backtrace steps
            {
                lastPosition = currentPosition;
                currentPosition = grid.GetCell(currentPosition).GetNeighboursLowestCost(grid.GetCell(lastPosition)).position;
                path.Push(currentPosition);
            }
            return new Visualization(grid, Conversion.ToArray(path), Conversion.ToPoints(checkedCells));
        }

        Visualization Dijkstra(Grid grid)
        {
            throw new NotImplementedException();
            Stack<Cell> path = new Stack<Cell>();

            
        }
    }
}
