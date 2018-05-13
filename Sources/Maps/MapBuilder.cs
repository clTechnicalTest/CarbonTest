namespace TreasureMap.Maps
{
	using System;
	using System.Collections.Generic;

	public class MapBuilder
	{
		private MapSizeEntry mapSizeEntry;
		private List<MapEntry> mapEntries = new List<MapEntry>();

		public MapBuilder(MapSizeEntry mapSizeEntry)
		{
			this.mapSizeEntry = mapSizeEntry;
		}

		public void AddEntries(IEnumerable<MapEntry> entries)
		{
			foreach (var item in entries)
			{
				var entry = item as IMapCoordinate;
				if (entry != null)
					mapEntries.Add(item);
			}
		}

		public Map GetMap()
		{
			Map newMap = new Map(mapSizeEntry.Width, mapSizeEntry.Height);

			PopulateMap(newMap, mapEntries);
			return newMap;
		}

		private static void PopulateMap(Map newMap, List<MapEntry> mapEntries)
		{
			foreach (var entry in mapEntries)
			{
				var adventurerEntry = entry as MapAdventurerEntry;
				if (adventurerEntry != null)
				{
					AdventurerCell cell = new AdventurerCell(adventurerEntry.Type, adventurerEntry.Name, new Coordinate(adventurerEntry.X, adventurerEntry.Y));
					newMap.AddAdventurer(cell);
				}
				else
				{
					Cell cell = new Cell(entry.Type, new Coordinate(((IMapCoordinate)entry).X, ((IMapCoordinate)entry).Y));
					newMap.AddCell(cell);
				}
			}
		}
	}
}