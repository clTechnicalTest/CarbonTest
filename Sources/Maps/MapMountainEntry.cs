using System;
namespace TreasureMap.Maps
{
	public class MapMountainEntry : MapEntry, IMapCoordinate
	{
		public MapMountainEntry(string type, string[] args)
			: base(type, args)
		{
			if (args.Length != 2)
				throw new ArgumentException("Invalid argument number", nameof(args));

			this.X = Convert.ToInt32(args[0]);
			this.Y = Convert.ToInt32(args[1]);
		}

		public int X { get; }
		public int Y { get; }
	}
}
