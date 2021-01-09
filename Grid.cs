using System;
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
        public Point startingPoint { get => startingPoint; set { GetCell(startingPoint = value).CellState = Cell.CellStates.StartingPoint; } }
        public Point targetPoint { get => targetPoint; set { GetCell(targetPoint = value).CellState = Cell.CellStates.TargetPoint; } }

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

        public Cell GetCell(int x, int y) => GetCell(new Point(x, y));

        public Cell GetCell(Point positionIndex) => columns[positionIndex.Y].GetCell(positionIndex.X);

        public string GetRepresented()
        {
            string output = "";
            for (int y = 0; y < size.Height; y++)
                for (int x = 0; x <= size.Width; x++)
                    if (x < size.Width)
                        output += GetCell(x, y).GetStateRepresentation();
                    else
                        output += "\n";
            return output;
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
        public int cost;
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

        /// <summary>
        /// returns an index based on its position, only recommended use is as an identifier
        /// </summary>
        public int GetIndex() => (parentColumn.Lenght - 1) * position.Y + position.X;

        public string GetStateRepresentation()
        {
            if (calculated)
                return cost.ToString();
            else
                switch (CellState)
                {
                    case CellStates.Blank:
                        return "- ";

                    case CellStates.Wall:
                        return "# ";

                    default:
                        throw new NotImplementedException();
                }
        }

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
                            output[i] = parentGrid.GetCell(new Point(position.X - i - 1, position.Y - 1));
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
                            output[i] = parentGrid.GetCell(new Point(position.X - (i - 6), position.Y + 1));
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

        /// <param name="excludedCell">Cell that won't be returned</param>
        public Cell GetNeighboursLowestCost(Cell excludedCell)
        {
            Cell[] neighbours = GetNeighbourCells();
            Cell output = null;
            int lowestCost = int.MaxValue;
            foreach (Cell cell in neighbours)
                if (cell.GetIndex() != excludedCell.GetIndex() && cell.calculated)
                    if (cell.cost < lowestCost)
                    {
                        output = cell;
                        lowestCost = cell.cost;
                    }
            return output;
        }

        public Cell GetNeighboursLowestCost() => GetNeighboursLowestCost(new Cell(new Point(int.MaxValue, int.MaxValue), parentColumn));
    }
}