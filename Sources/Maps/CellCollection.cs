namespace TreasureMap.Maps
{
	using System;
	using System.Collections.ObjectModel;

	public class CellCollection<T> : KeyedCollection<Coordinate, T> where T : Cell
	{
		protected override Coordinate GetKeyForItem(T item)
		{
			return item.Coordinate;
		}
	}
}
