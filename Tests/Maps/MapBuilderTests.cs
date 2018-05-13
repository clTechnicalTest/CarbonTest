namespace Tests.Maps
{
	using System;
	using NUnit.Framework;
	using TreasureMap.Maps;

	public class MapBuilderTests
	{
		[Test]
		public void BuildMapWithAMountain_GetMap_ReturnsAMapWithAMountain()
		{
			string expectedCellType = "M";
			int expectedXCoordinate = 2;
			int expectedYCoordinate = 3;
			MapMountainEntry mountainEntry = new MapMountainEntry(expectedCellType, new[] { expectedXCoordinate.ToString(), expectedYCoordinate.ToString() });

			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			builder.AddEntries(new[] { mountainEntry });
			Map result = builder.GetMap();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.GetCellType(expectedXCoordinate, expectedYCoordinate), Is.EqualTo(expectedCellType));
		}

		[Test]
		public void BuildMapWithAnAdventurer_GetMap_ReturnsAMap()
		{
			string expectedCellType = "A";
			int expectedXCoordinate = 2;
			int expectedYCoordinate = 3;
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", expectedXCoordinate.ToString(), expectedYCoordinate.ToString(), string.Empty, string.Empty });

			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			builder.AddEntries(new[] { adventurerEntry });
			Map result = builder.GetMap();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.GetCellType(expectedXCoordinate, expectedYCoordinate), Is.EqualTo(expectedCellType));
		}

		[Test]
		public void BuildMapWithAnAdventurerAndATreasureOnTheSameCell_GetMap_ReturnsAAdventurerType()
		{
			string expectedCellType = "A";
			int expectedXCoordinate = 2;
			int expectedYCoordinate = 3;
			MapTreasureEntry treasureEntry = new MapTreasureEntry("T", new[] { expectedXCoordinate.ToString(), expectedYCoordinate.ToString(), "2" });
			MapAdventurerEntry adventurerEntry = new MapAdventurerEntry("A", new[] { "Name", expectedXCoordinate.ToString(), expectedYCoordinate.ToString(), string.Empty, string.Empty });

			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			builder.AddEntries(new MapEntry[] { treasureEntry, adventurerEntry });
			Map result = builder.GetMap();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.GetCellType(expectedXCoordinate, expectedYCoordinate), Is.EqualTo(expectedCellType));
		}
	}
}
