namespace TreasureMap.Maps
{
	using System;
	using System.Collections.Generic;

	public partial class Map
	{

		private CellCollection mapItems = new CellCollection();
		private CellCollection adventurerItems = new CellCollection();

		public Map(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int Width { get; }
		public int Height { get; }

		public string GetCellType(int x, int y)
		{
			ValidateCoordinate(x, y);

			var coordinate = new Coordinate(x, y);
			string cellType = GetCellType(adventurerItems, coordinate);
			if (cellType == string.Empty){
				cellType = GetCellType(mapItems, coordinate);
			}
			return cellType;
		}

		private static string GetCellType(CellCollection cells, Coordinate coordinate){
			if (cells.Contains(coordinate))
            {
                var cell = cells[coordinate];
                return cell.Type;
            }
            return string.Empty;
		}
        
		internal void AddCell(Cell cell)
		{
			ValidateCoordinate(cell.Coordinate.X, cell.Coordinate.Y);

			mapItems.Add(cell);
		}

		private void ValidateCoordinate(int x, int y)
		{
			if (0 > x || x >= Width)
				throw new ArgumentOutOfRangeException(nameof(x));

			if (0 > y || y >= Height)
				throw new ArgumentOutOfRangeException(nameof(y));
		}

		internal void AddAdventurer(AdventurerCell cell)
		{
			ValidateCoordinate(cell.Coordinate.X, cell.Coordinate.Y);

			adventurerItems.Add(cell);
		}
	}
}
