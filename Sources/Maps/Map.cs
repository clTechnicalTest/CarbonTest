namespace TreasureMap.Maps
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public partial class Map : IMap
	{

		private CellCollection<Cell> mapItems = new CellCollection<Cell>();
		private CellCollection<AdventurerCell> adventurerItems = new CellCollection<AdventurerCell>();
		private List<AdventurerCell> adventurerCells = new List<AdventurerCell>();

		internal Map(int width, int height)
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
			if (cellType == string.Empty)
			{
				cellType = GetCellType(mapItems, coordinate);
			}
			return cellType;
		}

		private static string GetCellType<T>(CellCollection<T> cells, Coordinate coordinate)
			where T : Cell
		{
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
			adventurerCells.Add(cell);
		}

		public void MoveAdventurer(string name, int xStep, int yStep)
		{
			AdventurerCell cell = adventurerCells.Where(c => c.Name == name).First();
			int targetX = cell.Coordinate.X + xStep;
			int targetY = cell.Coordinate.Y + yStep;
			Coordinate targetCoordinate = new Coordinate(targetX, targetY);

			if (CheckAdventureCanMoveTo(targetCoordinate))
			{
				adventurerItems.Remove(cell);
				cell.UpdateCoordinate(targetX, targetY);
				adventurerItems.Add(cell);
			}
		}

		private bool CheckAdventureCanMoveTo(Coordinate targetCoordinate)
		{
			bool canMove = true;
			canMove &= !adventurerItems.Contains(targetCoordinate);
			canMove &= (mapItems.Contains(targetCoordinate) ? mapItems[targetCoordinate] : null)?.Type != "M";
			return canMove;
		}

		public string GetAdventurerName(int x, int y)
		{
			var coordinate = new Coordinate(x, y);
			return adventurerItems.Contains(coordinate) ? adventurerItems[coordinate].Name : string.Empty;
		}
	}
}
