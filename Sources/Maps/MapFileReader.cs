using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TreasureMap.Maps
{
	public class MapFileReader
	{
		public IEnumerable<MapEntry> GetEntries(Stream inputStream)
		{
			TextReader reader = new StreamReader(inputStream);
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				Console.WriteLine(line);
				if (!line.StartsWith('#'))
				{
					var splitedLine = line.Split('-');
					var type = splitedLine[0];
					var args = new string[splitedLine.Length - 1];
					Array.Copy(splitedLine, 1, args, 0, splitedLine.Length - 1);

					switch (type)
					{
						case "C":
							yield return new MapSizeEntry(type, args);
							break;

						case "T":
							yield return new MapTreasureEntry(type, args);
							break;

						case "M":
							yield return new MapMountainEntry(type, args);
							break;

						case "A":
							yield return new MapAdventurerEntry(type, args);
							break;
					}
				}
			}
		}
	}
}