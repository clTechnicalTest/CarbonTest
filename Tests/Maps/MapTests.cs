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
	}
}
