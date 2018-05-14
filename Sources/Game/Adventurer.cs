using System;
using System.Collections;
using System.Collections.Generic;
using TreasureMap.Maps;

namespace TreasureMap.Game
{
	public class Adventurer
	{
		public const char COMMAND_RIGHT = 'D';
		public const char COMMAND_LEFT = 'G';
		public const char COMMAND_PROGRESS = 'A';
		public const string ORIENTATION_NORTH = "N";
		public const string ORIENTATION_WEST = "O";
		public const string ORIENTATION_SOUTH = "S";
		public const string ORIENTATION_EAST = "E";

		private static List<string> orientations = new List<string>() { ORIENTATION_NORTH, ORIENTATION_WEST, ORIENTATION_SOUTH, ORIENTATION_EAST };
		private IMap map;
		private Queue<char> movements;

		public Adventurer(IMap map, MapAdventurerEntry adventurerEntry)
		{
			this.map = map;
			this.movements = new Queue<char>(adventurerEntry.Movements.ToCharArray());
			this.Orientation = adventurerEntry.Orientation;
			this.Name = adventurerEntry.Name;
		}

		public string Orientation { get; private set; }
		public string Name { get; }
		public bool HasAction
		{
			get{
				return movements.Count != 0;
			}
		}

		public void Move()
		{
			char command = movements.Dequeue();
			switch (command)
			{
				case COMMAND_PROGRESS:
					Progress();
					break;

				case COMMAND_RIGHT:
				case COMMAND_LEFT:
					Orientation = GetNewOrientation(command);
					break;
			}
		}

		private void Progress()
		{
			var (x, y) = GetVectorMovement();
			map.MoveAdventurer(Name, x, y);
		}

		private (int x, int y) GetVectorMovement()
		{
			int xStep = 0;
			int yStep = 0;

			switch (Orientation)
			{
				case ORIENTATION_NORTH:
					yStep = -1;
					break;

				case ORIENTATION_SOUTH:
					yStep = 1;
					break;

				case ORIENTATION_EAST:
					xStep = -1;
					break;

				case ORIENTATION_WEST:
					xStep = 1;
					break;
			}
			return (xStep, yStep);
		}

		private string GetNewOrientation(char command)
		{
			string newOrientation = string.Empty;
			int currentIndex = orientations.IndexOf(Orientation);
			int newIndex = currentIndex;

			if (command == COMMAND_RIGHT)
				newIndex = GetIndexOnRollingList(orientations, currentIndex + 1);
			else if (command == COMMAND_LEFT)
				newIndex = GetIndexOnRollingList(orientations, currentIndex - 1);
			return orientations[newIndex];
		}

		private int GetIndexOnRollingList(ICollection list, int index)
		{
			if (index < 0)
				return list.Count + index;
			if (index > list.Count - 1)
				return index - list.Count;
			return index;
		}
	}
}
