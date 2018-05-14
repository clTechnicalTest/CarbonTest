using System;
using TreasureMap.Maps;

namespace TreasureMap.Game
{
	public class Controller
	{
		public Controller(Map map, Adventurer[] adventurers)
		{
			Map = map;
			Adventurers = adventurers;
		}

		public Map Map { get; }
		public Adventurer[] Adventurers { get; }

		public void RunGameLoop()
		{
			while (true)
			{
				bool actionLeft = false;

				foreach (var adventurer in Adventurers)
				{
					actionLeft |= adventurer.HasAction;
					if (adventurer.HasAction)
						adventurer.Move();
				}
				if (actionLeft)
					break;
			}
		}
	}
}