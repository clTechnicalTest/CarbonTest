using System;
namespace TreasureMap.Maps
{
    public interface IMap
    {
		string GetCellType(int x, int y);
		void MoveAdventurer(string name, int xStep, int yStep);
    }
}
