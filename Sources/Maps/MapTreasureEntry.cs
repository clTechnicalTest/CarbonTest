using System;
namespace TreasureMap.Maps
{
	public class MapTreasureEntry : MapEntry, IMapCoordinate
	{
		public MapTreasureEntry(string type, string[] args)
			: base(type, args)
		{
			if (args.Length != 3)
				throw new ArgumentException("Invalid arguments number", nameof(args));
			
			this.X = Convert.ToInt32(args[0]);
			this.Y = Convert.ToInt32(args[1]);
			this.TreasureCount = Convert.ToInt32(args[2]);
		}
        
		public int X { get; }
		public int Y { get; }
		public int TreasureCount { get; }
	}
}
