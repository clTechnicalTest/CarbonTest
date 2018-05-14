namespace Tests
{
	using System;
	using NUnit.Framework;
	using TreasureMap.Maps;

	public class MapTests
	{
		[Test]
		public void BuildMapWithAMountain_GetCellType_ReturnsEmptyCellType()
		{
			string expectedCellType = string.Empty;
			string mountainCellType = "M";
			int mountainXCoordinate = 2;
			int mountainYCoordinate = 3;
			MapMountainEntry mountainEntry = new MapMountainEntry(mountainCellType, new[] { mountainXCoordinate.ToString(), mountainYCoordinate.ToString() });
			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			builder.AddEntries(new[] { mountainEntry });
			Map map = builder.GetMap();

			var result = map.GetCellType(4, 4);

			Assert.That(result, Is.EqualTo(expectedCellType));
		}

		[Test]
		public void BuildEmptyMap_GetCellType_ThrowsOutOfRangeExceptionWhenCoordinateExceedRange()
		{
			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			Map map = builder.GetMap();

			Assert.That(() => map.GetCellType(10, 10), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void AdventurerAlone_MoveAdventurer_AdventurerMovedToWest()
		{
			string adventurerName = "adv";
			int initialX = 1;
			int initialY = 1;
			int stepX = 1;
			int stepY = 0;
			int expectedX = 2;
			int expectedY = 1;
			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			builder.AddEntries(new[] { new MapAdventurerEntry("A", new[] { adventurerName, initialX.ToString(), initialY.ToString(), "O", string.Empty }) });
			Map map = builder.GetMap();

			string initialType = map.GetCellType(expectedX, expectedY);
			map.MoveAdventurer(adventurerName, stepX, stepY);
			string finalType = map.GetCellType(expectedX, expectedY);

			Assert.That(initialType, Is.EqualTo(string.Empty));
			Assert.That(finalType, Is.EqualTo("A"));
		}

		[Test]
		public void TwoAdventurersBesideOneTheMap_MoveAdventurer_AdventurerCantGoOnTheOtherAdventurerCell()
		{
			string adventurerName1 = "adv1";
			int advName1X = 1;
			int advName1Y = 1;
			int advName1StepX = 1;
			int advName1StepY = 0;
			string adventurerName2 = "adv2";
			int advName2X = 2;
			int advName2Y = 1;

			MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
			builder.AddEntries(new[]
			{
				new MapAdventurerEntry("A", new[] { adventurerName1, advName1X.ToString(), advName1Y.ToString(), "O", string.Empty }),
                new MapAdventurerEntry("A", new[] { adventurerName2, advName2X.ToString(), advName2Y.ToString(), "O", string.Empty })
			});
			Map map = builder.GetMap();

			map.MoveAdventurer(adventurerName1, advName1StepX, advName1StepY);
			string advNameAtStartPositionName1 = map.GetAdventurerName(advName1X, advName1Y);
			string advNameAtStartPositionName2 = map.GetAdventurerName(advName2X, advName2Y);

			Assert.That(advNameAtStartPositionName1, Is.EqualTo(adventurerName1));
			Assert.That(advNameAtStartPositionName2, Is.EqualTo(adventurerName2));
		}

		[Test]
        public void AnAdventurerAndMountainBesideOneTheMap_MoveAdventurer_AdventurerCantGoOnTheMountain()
        {
            string adventurerName1 = "adv1";
            int advName1X = 1;
            int advName1Y = 1;
            int advName1StepX = 1;
            int advName1StepY = 0;
			int mountainX = 2;
			int mountainY = 1;

            MapBuilder builder = new MapBuilder(new MapSizeEntry("C", new[] { "5", "5" }));
            builder.AddEntries(new MapEntry[]
            {
                new MapAdventurerEntry("A", new[] { adventurerName1, advName1X.ToString(), advName1Y.ToString(), "O", string.Empty }),
                new MapMountainEntry("M", new[] { mountainX.ToString(), mountainY.ToString() })
            });
            Map map = builder.GetMap();

            map.MoveAdventurer(adventurerName1, advName1StepX, advName1StepY);
            string advNameAtStartPositionName1 = map.GetAdventurerName(advName1X, advName1Y);

            Assert.That(advNameAtStartPositionName1, Is.EqualTo(adventurerName1));
        }
	}
}
