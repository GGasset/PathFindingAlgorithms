using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingAlgorithms
{
    /// <summary>
    /// Everithing needed to display every step of a pathfinding algorithm
    /// </summary>
    class Visualization
    {
        /*Algorithm visualization:
          1)Draw all cells and cellStates
          2)Change color of checked cells in order
          3)Chage color of path*/
        Grid visualicedGrid;
        Point[] path;
        Queue<Point> checkedCells;
        
        /// <param name="grid">Grid after running an algorithm</param>
        /// <param name="path">Path from starting-point to target-point.</param>
        /// <param name="checkedCells">Cells that have been referenced</param>
        public Visualization(Grid grid, Point[] path, Queue<Point> checkedCells)
        {
            visualicedGrid = grid;
            this.path = path;
            this.checkedCells = checkedCells;
        }
    }
}
