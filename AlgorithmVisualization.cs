using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFindingAlgorithms
{
    /// <summary>
    /// Everithing needed to display every step of a pathfinding algorithm
    /// </summary>
    internal class Visualization
    {
        /*Algorithm visualization:
          1)Draw all cells and cellStates
          2)Change color of checked cells in order
          3)Change color of path*/
        private Grid visualicedGrid;
        private Point[] path;
        private List<Point> checkedCells;

        /// <param name="grid">Grid after running an algorithm</param>
        /// <param name="path">Path from starting-point to target-point.</param>
        /// <param name="checkedCells">Cells that have been referenced</param>
        public Visualization(Grid grid, Point[] path, List<Point> checkedCells)
        {
            visualicedGrid = grid;
            this.path = path;
            this.checkedCells = checkedCells;
        }

        public string GetGridString()
        {
            string output = "";
            for (int y = 0; y < visualicedGrid.size.Height; y++)
                for (int x = 0; x <= visualicedGrid.size.Width; x++)
                    if (x < visualicedGrid.size.Width)
                        output += GetCellString(visualicedGrid.GetCell(x, y));
                    else
                        output += "\n";
            return output;
        }

        string GetCellString(Cell cell)
        {
            string pathString = "# ", checkedCellsString = "~ ";
            if (cell.position == visualicedGrid.GetStartPos() || cell.position == visualicedGrid.GetTargetPos())
                return GetCellStateRepresentation(cell);

            if (ArrayContains(path, cell.position))
                return pathString;

            if (checkedCells.Contains(cell.position))
                return checkedCellsString;
            
            return GetCellStateRepresentation(cell);
            bool ArrayContains(Point[] array, Point value)
            {
                foreach (var point in array)
                    if (value == point)
                        return true;
                return false;
            }
        }

        public static string GetCellStateRepresentation(Cell cell)
        {
            switch (cell.CellState)
            {
                case Cell.CellStates.StartingPoint:
                    return "A ";

                case Cell.CellStates.TargetPoint:
                    return "B ";

                case Cell.CellStates.Blank:
                    return "  ";

                case Cell.CellStates.Wall:
                    return "| ";

                default:
                    throw new NotImplementedException();
            }
        }
    }
}