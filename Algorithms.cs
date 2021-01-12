using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFindingAlgorithms
{
    internal class Algorithms
    {
        public static Visualization AStar(Grid grid)
        {
            Stack<Point> path = new Stack<Point>();
            List<Cell> checkedCells = new List<Cell>();
            Point currentPosition = grid.GetStartPos(), lastPosition = grid.GetStartPos();
            List<Cell> toEvaluate = new List<Cell>{ grid.GetCell(grid.GetStartPos()) }, evaluated = new List<Cell>();
            grid.GetCell(toEvaluate[0].position).cost = GetCost(toEvaluate[0].position);

            do
            {
                if (toEvaluate.Count == 0)//Agorithm failed, no path
                    return new Visualization(grid, null, Conversion.ToPoints(checkedCells));

                int lowestCost = int.MaxValue, lowestCostToevaluateIndex = int.MaxValue;
                for (int i = 0; i < toEvaluate.Count; i++) //Get Lowest cost in toEvaluate and calculate it if needed
                {
                    if (!toEvaluate[i].calculated)
                    {
                        toEvaluate[i].startDistance = GetDistance(toEvaluate[i].position, grid.GetStartPos());
                        toEvaluate[i].targetDistance = GetDistance(toEvaluate[i].position, grid.GetTargetPos());
                        toEvaluate[i].cost = GetCost(toEvaluate[i].position);
                        toEvaluate[i].calculated = true;

                        //Update changes in grid
                        grid.GetCell(toEvaluate[i].position).startDistance = toEvaluate[i].startDistance;
                        grid.GetCell(toEvaluate[i].position).targetDistance = toEvaluate[i].targetDistance;
                        grid.GetCell(toEvaluate[i].position).cost = toEvaluate[i].cost;
                        grid.GetCell(toEvaluate[i].position).calculated = true;
                    }
                    if (toEvaluate[i].cost <= lowestCost)
                    {
                        lowestCostToevaluateIndex = i;
                        lowestCost = toEvaluate[i].cost;
                    }
                }
                currentPosition = toEvaluate[lowestCostToevaluateIndex].position;
                evaluated.Add(toEvaluate[lowestCostToevaluateIndex]);
                checkedCells.Add(toEvaluate[lowestCostToevaluateIndex]);
                toEvaluate.RemoveAt(lowestCostToevaluateIndex);

                if (currentPosition != grid.GetTargetPos())
                {
                    Cell[] uncheckedNeighbourCells = grid.GetCell(currentPosition).GetNeighbourCells(joinLists(evaluated, toEvaluate));
                    toEvaluate.AddRange(uncheckedNeighbourCells);
                    checkedCells.AddRange(uncheckedNeighbourCells);
                }

            } while (currentPosition != grid.GetTargetPos());

            path.Push(currentPosition);
            while (currentPosition != grid.GetStartPos()) //Backtrace steps
            {
                lastPosition = currentPosition;
                currentPosition = GetLowestStartDistanceAdjacentInPosition(evaluated, currentPosition, Conversion.ToCellList(path, grid));
                //Console.Write("");

                path.Push(currentPosition);
            }
            return new Visualization(grid, Conversion.ToArray(path), Conversion.ToPoints(checkedCells));

            #region Local Functions
            int GetCost(Point position)
            {
                int distanceToStart, distanceToTarget;
                distanceToStart = Math.Abs(GetDistance(position, grid.GetStartPos()));
                distanceToTarget = Math.Abs(GetDistance(position, grid.GetTargetPos()));
                int cost = distanceToStart + distanceToTarget;
                return cost;
            }

            int GetDistance(Point from, Point to)
            {
                Point difference = new Point(Math.Abs(to.X - from.X), Math.Abs(to.Y - from.Y));
                Point minMax = GetMinimumAndMaximum(difference);
                int diagonalDistance = (minMax.X) * 14;
                int nonDiagonalDistance = Math.Abs(minMax.X - minMax.Y) * 10;
                int distance = diagonalDistance + nonDiagonalDistance;
                return distance;

                Point GetMinimumAndMaximum(Point point) => new Point(Math.Min(point.X, point.Y), Math.Max(point.X, point.Y));
            }

            List<Cell> joinLists(List<Cell> firstList, List<Cell> secondList)
            {
                List<Cell> output = new List<Cell>();
                output.AddRange(firstList);
                output.AddRange(secondList);
                return output;
            }

            Point GetLowestStartDistanceAdjacentInPosition(List<Cell> list, Point currenPos, List<Cell> excludedCells)
            {
                Point output = new Point();
                int lowestDistance = int.MaxValue;
                foreach (Cell cell in list)
                    if (!excludedCells.Contains(cell) && lowestDistance > cell.startDistance)
                    {
                        Point difference = new Point(Math.Abs(cell.position.X - currenPos.X), Math.Abs(cell.position.Y - currenPos.Y));
                        if (difference != Point.Empty)
                            if (difference.X <= 1 && difference.Y <= 1)
                            {
                                lowestDistance = cell.startDistance;
                                output = cell.position;
                            }

                    }
                return output;
            }
            #endregion Local Functions
        }

        private Visualization Dijkstra(Grid grid)
        {
            throw new NotImplementedException();
            Stack<Cell> path = new Stack<Cell>();
        }
    }
}