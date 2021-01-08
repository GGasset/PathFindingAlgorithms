using System;
using System.Drawing;

namespace PathFindingAlgorithms
{
    /// <summary>
    /// Grid made to support implementation of any path finding algorithms
    /// </summary>
    class Grid
    {
        private Column[] columns;
        private Size size;
        public Point startingPoint, targetPoint;

        public Grid(int width, int height) => new Grid(new Size(width, height));

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

    class Column
    {
        public Grid parentGrid;
        private Cell[] cells;
        int columnIndex;
        public int Lenght { get => cells.Length; }

        public Column(int columnIndex, int width, Grid parentGrid) 
        {
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

    class Cell
    {
        private Grid parentGrid;
        private Column parentColumn;
        public readonly Point location;
        public CellStates CellState = CellStates.Blank;
        public int cost;

        public enum CellStates
	    {
            Blank,
            Wall,
            CalculatedBlank,
	    }

        public string GetStateRepresentation()
        {
            switch (CellState)
	        {
		        case CellStates.Blank:
                    return "- ";
                 break;
                case CellStates.Wall:
                    return "# ";
                 break;
                case CellStates.CalculatedBlank:
                    return $"{cost.ToString()} ";
                 break;
                default:
                    throw new NotImplementedException();
                 break;
	}
        }

        public Cell(Point location, Column parent)
        {
            this.location = location;
            parentColumn = parent;
            parentGrid = parent.parentGrid;
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
                            output[i] = parentGrid.GetCell(new Point(location.X - i - 1, location.Y - 1));
                            break;

                        case 3://Sides
                            output[i] = parentColumn.GetCell(location.X - 1);
                            break;

                        case 4:
                            output[i] = parentColumn.GetCell(location.X + 1);
                            break;

                        case 5://Bottom
                        case 6:
                        case 7:
                            output[i] = parentGrid.GetCell(new Point(location.X - (i - 6), location.Y + 1));
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
    }
}