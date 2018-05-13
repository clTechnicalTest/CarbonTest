using System;
namespace TreasureMap.Maps
{
	public class MapAdventurerEntry : MapEntry, IMapCoordinate
	{
		public MapAdventurerEntry(string type, string[] args)
			: base(type, args)
		{
			if (args.Length != 5)
				throw new ArgumentException("Invalid arguments number", nameof(args));
			
			this.Name = args[0];
			this.X = Convert.ToInt32(args[1]);
			this.Y = Convert.ToInt32(args[2]);
			this.Orientation = args[3];
			this.Movements = args[4];
		}

		public string Name { get; }
		public int X { get; }
		public int Y { get; }
		public string Orientation { get; }
		public string Movements { get; }
	}
}
