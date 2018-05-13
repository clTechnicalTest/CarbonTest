namespace TreasureMap.Maps
{
    public class Cell
    {
		public Coordinate Coordinate { get; }

        public Cell(string type, Coordinate coordinate)
        {
			this.Coordinate = coordinate;
			this.Type = type;
		}

		public string Type { get; }
	}
}