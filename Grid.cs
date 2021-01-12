using System;
using System.Collections.Generic;
using System.Drawing;

namespace PathFindingAlgorithms
{
    /// <summary>
    /// Grid made to support implementation of any path finding algorithms
    /// </summary>
    internal class Grid
    {
        private Column[] columns;
        public readonly Size size;
        private Point startingPosition;
        private Point targetPosition;

        public Grid(int width, int height)
        {
            Grid grid = new Grid(new Size(width, height));
            columns = grid.columns;
            size = grid.size;
        }

        public Grid(Size size)
        {
            this.size = size;
            columns = new Column[size.Height];
            for (int i = 0; i < size.Height; i++)
                columns[i] = new Column(i, size.Width, this);
        }

        public string GetRepresented()
        {
            string output = "";
            for (int y = 0; y < size.Height; y++)
                for (int x = 0; x <= size.Width; x++)
                    if (x < size.Width)
                        output += Visualization.GetCellStateRepresentation(GetCell(x, y));
                    else
                        output += "\n";
            return output;
        }

        public Point GetStartPos() => startingPosition;

        public Point GetTargetPos() => targetPosition;

        public void SetStartPos(Point position) => SetCellstate(startingPosition = position, Cell.CellStates.StartingPoint);

        public void SetTargetPos(Point position) => SetCellstate(targetPosition = position, Cell.CellStates.TargetPoint);

        public Cell GetCell(int x, int y) => GetCell(new Point(x, y));

        public Cell GetCell(Point positionIndex) => columns[positionIndex.Y].GetCell(positionIndex.X);

        public Cell.CellStates SetCellstate(Point position, Cell.CellStates state)
        {
            return GetCell(position).CellState = state;
        }
    }

    internal class Column
    {
        public Grid parentGrid;
        private Cell[] cells;
        public int columnIndex { get; }
        public int Lenght { get => cells.Length; }

        public Column(int columnIndex, int width, Grid parentGrid)
        {
            this.columnIndex = columnIndex;
            this.parentGrid = parentGrid;
            cells = new Cell[width];
            for (int i = 0; i < width; i++)
            {
                Point cellLocation = new Point(i, this.columnIndex);
                cells[i] = new Cell(cellLocation, this);
            }
        }

        public Cell GetCell(int index) => cells[index];
    }

    internal class Cell
    {
        private Grid parentGrid;
        private Column parentColumn;
        public readonly Point position;
        public CellStates CellState = CellStates.Blank;
        public int cost = int.MaxValue, startDistance = int.MaxValue, targetDistance = int.MaxValue;
        public bool calculated = false;

        public enum CellStates
        {
            Blank,
            Wall,
            StartingPoint,
            TargetPoint,
        }

        public Cell(Point position, Column parent)
        {
            this.position = position;
            parentColumn = parent;
            parentGrid = parent.parentGrid;
        }

        public override string ToString()
        {
            string cost = (this.cost == int.MaxValue) ? null : this.cost.ToString();
            return $"Position: {position}, Cost: {cost}, State: {CellState}";
        }

        /// <summary>
        /// returns an index based on its position, only recommended use is as an identifier
        /// </summary>
        public int GetIndex() => (parentColumn.Lenght - 1) * position.Y + position.X;

        public Cell[] GetNeighbourCells()
        {
            Cell[] output = new Cell[8];
            for (int i = 0; i < output.Length; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0://Top
                        case 1:
                        case 2:
                            output[i] = parentGrid.GetCell(new Point(position.X + i - 1, position.Y + 1));
                            break;

                        case 3://Sides
                            output[i] = parentColumn.GetCell(position.X - 1);
                            break;

                        case 4:
                            output[i] = parentColumn.GetCell(position.X + 1);
                            break;

                        case 5://Bottom
                        case 6:
                        case 7:
                            output[i] = parentGrid.GetCell(new Point(position.X + i - 6, position.Y - 1));
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    output[i] = null;
                }
            }
            return output;
        }

        public Cell[] GetNeighbourCells(List<Cell> excludedCells)
        {
            Cell[] neighbours = GetNeighbourCells();
            Queue<Cell> output = new Queue<Cell>();
            foreach (Cell neighbour in neighbours)
            {
                bool isExcluded = false;
                if (neighbour != null && neighbour.CellState != CellStates.Wall)
                {
                    foreach (Cell excludedCell in excludedCells)
                        if (excludedCell.position == neighbour.position)
                        {
                            isExcluded = true;
                            break;
                        }
                    //isExcluded = excludedCell.position == neighbour.position && !isExcluded;
                    if (!isExcluded)
                        output.Enqueue(neighbour);
                }
            }
            return Conversion.ToArray(output);
        }

        /// <param name="excludedCell">Cells that won't be returned</param>
        public Cell GetNeighboursLowestCost(List<Cell> excludedCells)
        {
            Cell[] neighbours = parentGrid.GetCell(position).GetNeighbourCells(excludedCells);
            Cell output = null;
            int lowestCost = int.MaxValue;
            foreach (Cell cell in neighbours)
                if (cell != null && cell.calculated)
                    if (cell.cost < lowestCost)
                    {
                        output = cell;
                        lowestCost = cell.cost;
                    }
            return output;
        }

        public Cell GetNeighboursLowestCost() => GetNeighboursLowestCost(new List<Cell>());
    }
}