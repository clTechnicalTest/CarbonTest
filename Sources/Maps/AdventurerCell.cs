namespace TreasureMap.Maps
{
	internal class AdventurerCell:Cell
	{

		public AdventurerCell(string type, string name, Coordinate coordinate)
			: base(type, coordinate)
		{
			this.Name = name;
		}

		public string Name { get; }

		public void UpdateCoordinate(int x, int y){
			Coordinate coordinate = new Coordinate(x, y);
			this.Coordinate = coordinate;
		}
	}
}