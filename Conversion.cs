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
            for (int i = 0; i < cells.Count; i++)
                output[i] = cells.Pop().position;
            return output;
        }

        public static Queue<Point> ToPoints(Queue<Cell> cells)
        {
            Queue<Point> output = new Queue<Point>();
            for (int i = 0; i < cells.Count; i++)
                output.Enqueue(cells.Dequeue().position);
            return output;
        }

        public static Point[] ToArray(Stack<Point> points)
        {
            Point[] output = new Point[points.Count];
            for (int i = 0; i < points.Count; i++)
                output[i] = points.Pop();
            return output;
        }
    }
}
