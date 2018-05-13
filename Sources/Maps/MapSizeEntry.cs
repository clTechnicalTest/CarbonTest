using System;

namespace TreasureMap.Maps
{
	public class MapSizeEntry : MapEntry
	{
		public MapSizeEntry(string type, string[] args)
			: base(type, args)
		{
			if (args.Length != 2)
				throw new ArgumentException("Invalid parameters count", nameof(args));
			
			this.Width = Convert.ToInt32(args[0]);
			this.Height = Convert.ToInt32(args[1]);
		}

		public int Width { get; }
		public int Height { get; }
	}
}
