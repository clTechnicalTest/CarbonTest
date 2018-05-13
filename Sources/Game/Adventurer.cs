using System;
using System.Collections;
using System.Collections.Generic;
using TreasureMap.Maps;

namespace TreasureMap.Game
{
	public class Adventurer
	{
		private static  List<string> orientations = new List<string>() { "N", "O", "S", "E" };
		private Map map;
		private Queue<char> movements;

		public Adventurer(Map map, MapAdventurerEntry adventurerEntry)
		{
			this.map = map;
			this.movements = new Queue<char>(adventurerEntry.Movements.ToCharArray());
			this.Orientation = adventurerEntry.Orientation;
			this.Name = adventurerEntry.Name;
		}

		public string Orientation { get; private set; }
		public string Name { get; }

		public void Move()
		{
			char command = movements.Dequeue();
			Orientation = GetNewOrientation(command);
		}

		private string GetNewOrientation(char command)
		{
			string newOrientation = string.Empty;
			int currentIndex = orientations.IndexOf(Orientation);
			int newIndex = currentIndex;

			if (command == 'D')
				newIndex = GetIndexOnRollingList(orientations, currentIndex + 1);
			else if (command == 'G')
				newIndex = GetIndexOnRollingList(orientations, currentIndex - 1);
			return orientations[newIndex];
		}

		private int GetIndexOnRollingList(ICollection list, int index){
			if (index < 0)
				return list.Count + index;
			if (index > list.Count - 1)
				return index - list.Count;
			return index;
		}
	}
}
