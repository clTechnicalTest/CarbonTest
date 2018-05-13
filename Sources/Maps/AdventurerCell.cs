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
	}
}