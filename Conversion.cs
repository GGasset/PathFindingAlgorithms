using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingAlgorithms
{
    static class Conversion
    {
        public static Point[] ToPoints(Stack<Cell> cells)
        {
            Point[] output = new Point[cells.Count];
            for (int i = 0; i < output.Length; i++)
                output[i] = cells.Pop().position;
            return output;
        }

        public static List<Point> ToPoints(List<Cell> cells)
        {
            int initialCount = cells.Count;
            List<Point> output = new List<Point>();
            for (int i = 0; i < initialCount; i++)
                output.Add(cells[i].position);
            return output;
        }

        public static Point[] ToArray(Stack<Point> points)
        {
            Point[] output = new Point[points.Count];
            for (int i = 0; i < output.Length; i++)
                output[i] = points.Pop();
            return output;
        }

        public static Cell[] ToArray(Queue<Cell> cells)
        {
            Cell[] output = new Cell[cells.Count];
            for (int i = 0; i < output.Length; i++)
                output[i] = cells.Dequeue();
            return output;
        }

        public static List<Cell> ToCellList(Stack<Point> points, Grid parentGrid)
        {
            List<Cell> output = new List<Cell>();
            foreach (Point position in points)
                output.Add(parentGrid.GetCell(position));
            return output;
        }

    }
}
