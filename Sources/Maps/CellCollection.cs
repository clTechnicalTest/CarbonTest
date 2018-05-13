namespace TreasureMap.Maps
{
	using System;
	using System.Collections.ObjectModel;

	public class CellCollection : KeyedCollection<Coordinate, Cell>
	{
		protected override Coordinate GetKeyForItem(Cell item)
		{
			return item.Coordinate;
		}
	}
}
