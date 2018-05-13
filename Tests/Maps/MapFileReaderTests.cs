using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using TreasureMap.Maps;

namespace Tests.Maps
{
	public class MapFileReaderTests
	{
		private void AppendString(Stream stream, string input)
		{
			TextWriter writer = new StreamWriter(stream);
			writer.WriteLine(input);
			writer.Flush();
		}

		[Test]
		public void ReadAWellFormattedLine_ReturnsOneMapEntryWithRightType()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "C";
				AppendString(inputStream, $"{expectedType}-2-3");
				inputStream.Position = 0;

				MapFileReader fileReader = new MapFileReader();
				IEnumerable<MapEntry> result = fileReader.GetEntries(inputStream).ToList();

				Assert.That(result, Is.Not.Null);
				Assert.That(result.Count(), Is.EqualTo(1));
				Assert.That(result.First().Type, Is.EqualTo(expectedType));
			}
		}

		[Test]
		public void ReadACommentLine_ReturnsAnEmptyCollection()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				AppendString(inputStream, "# Super comment !");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();
				var result = reader.GetEntries(inputStream);

				Assert.That(result, Is.Not.Null);
				Assert.That(result.Count, Is.EqualTo(0));
			}
		}

		[Test]
		public void ReadAMapLine_ReturnsAMapEntryWithPopulatedProperties()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "C";
				int expectedWidth = 4;
				int expectedHeight = 3;
				AppendString(inputStream, $"{expectedType}-{expectedWidth}-{expectedHeight}");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();
				var result = reader.GetEntries(inputStream);
				var firstEntryResult = result.FirstOrDefault();

				Assert.That(firstEntryResult, Is.Not.Null);
				Assert.That(firstEntryResult, Is.TypeOf(typeof(MapSizeEntry)));
				Assert.That(((MapSizeEntry)firstEntryResult).Width, Is.EqualTo(expectedWidth));
				Assert.That(((MapSizeEntry)firstEntryResult).Height, Is.EqualTo(expectedHeight));
			}
		}

		[Test]
		public void ReadABadFormattedMapLine_ThrowsInvalidArgument()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "C";
				int expectedWidth = 4;
				int expectedHeight = 3;
				AppendString(inputStream, $"{expectedType}-{expectedWidth}-{expectedHeight}-xx");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();

				Assert.That(() => reader.GetEntries(inputStream).ToList(), Throws.Exception.TypeOf<ArgumentException>());
			}
		}

		[Test]
		public void ReadATreasureLine_ReturnsATreasureEntryWithPopulatedProperties()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "T";
				int expectedWidth = 4;
				int expectedHeight = 3;
				int expectedTreasureNumber = 2;
				AppendString(inputStream, $"{expectedType}-{expectedWidth}-{expectedHeight}-{expectedTreasureNumber}");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();
				var result = reader.GetEntries(inputStream);
				var firstEntryResult = result.FirstOrDefault();

				Assert.That(firstEntryResult, Is.Not.Null);
				Assert.That(firstEntryResult, Is.TypeOf(typeof(MapTreasureEntry)));
				Assert.That(((MapTreasureEntry)firstEntryResult).X, Is.EqualTo(expectedWidth));
				Assert.That(((MapTreasureEntry)firstEntryResult).Y, Is.EqualTo(expectedHeight));
				Assert.That(((MapTreasureEntry)firstEntryResult).TreasureCount, Is.EqualTo(expectedTreasureNumber));
			}
		}

		[Test]
		public void ReadABadFormattedTreasureLine_ThrowsInvalidArgument()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "T";
				int expectedWidth = 4;
				int expectedHeight = 3;
				int expectedTreasureNumber = 2;
				AppendString(inputStream, $"{expectedType}-{expectedWidth}-{expectedHeight}-{expectedTreasureNumber}-xx");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();

				Assert.That(() => reader.GetEntries(inputStream).ToList(), Throws.Exception.TypeOf<ArgumentException>());
			}
		}

		[Test]
		public void ReadAMountainLine_ReturnsAMountainEntryWithPopulatedProperties()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "M";
				int expectedWidth = 4;
				int expectedHeight = 3;
				AppendString(inputStream, $"{expectedType}-{expectedWidth}-{expectedHeight}");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();
				var result = reader.GetEntries(inputStream);
				var firstEntryResult = result.FirstOrDefault();

				Assert.That(firstEntryResult, Is.Not.Null);
				Assert.That(firstEntryResult, Is.TypeOf(typeof(MapMountainEntry)));
				Assert.That(((MapMountainEntry)firstEntryResult).X, Is.EqualTo(expectedWidth));
				Assert.That(((MapMountainEntry)firstEntryResult).Y, Is.EqualTo(expectedHeight));
			}
		}

		[Test]
		public void ReadABadFormattedMountainLine_ThrowsInvalidArgument()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "M";
				int expectedWidth = 4;
				int expectedHeight = 3;
				AppendString(inputStream, $"{expectedType}-{expectedWidth}-{expectedHeight}-xx");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();

				Assert.That(() => reader.GetEntries(inputStream).ToList(), Throws.Exception.TypeOf<ArgumentException>());
			}
		}

		[Test]
		public void ReadAnAdventurerLine_ReturnsAnAdventurerEntryWithPopulatedProperties()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "A";
				string expectedName = "Anna";
				int expectedWidth = 4;
				int expectedHeight = 3;
				string expectedOrientation = "S";
				string expectedMovements = "AGAADAA";
				AppendString(inputStream, $"{expectedType}-{expectedName}-{expectedWidth}-{expectedHeight}-{expectedOrientation}-{expectedMovements}");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();
				var result = reader.GetEntries(inputStream);
				var firstEntryResult = result.FirstOrDefault();

				Assert.That(firstEntryResult, Is.Not.Null);
				Assert.That(firstEntryResult, Is.TypeOf(typeof(MapAdventurerEntry)));
				Assert.That(((MapAdventurerEntry)firstEntryResult).Name, Is.EqualTo(expectedName));
				Assert.That(((MapAdventurerEntry)firstEntryResult).X, Is.EqualTo(expectedWidth));
				Assert.That(((MapAdventurerEntry)firstEntryResult).Y, Is.EqualTo(expectedHeight));
				Assert.That(((MapAdventurerEntry)firstEntryResult).Orientation, Is.EqualTo(expectedOrientation));
				Assert.That(((MapAdventurerEntry)firstEntryResult).Movements, Is.EqualTo(expectedMovements));
			}
		}

		[Test]
		public void ReadABadFormattedAdventurerLine_ThrowsInvalidArgument()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				string expectedType = "A";
				string expectedName = "Anna";
				int expectedWidth = 4;
				int expectedHeight = 3;
				string expectedOrientation = "S";
				string expectedMovements = "AGAADAA";
				AppendString(inputStream, $"{expectedType}-{expectedName}-{expectedWidth}-{expectedHeight}-{expectedOrientation}-{expectedMovements}-xx");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();

				Assert.That(() => reader.GetEntries(inputStream).ToList(), Throws.Exception.TypeOf<ArgumentException>());
			}
		}

		[Test]
		public void Read3WellFormattedLine_Returns3MapEntry()
		{
			using (MemoryStream inputStream = new MemoryStream())
			{
				int expectedEntriesCount = 3;
				AppendString(inputStream, "C-3-3");
				AppendString(inputStream, "M-1-1");
				AppendString(inputStream, "T-2-2-2");
				inputStream.Position = 0;

				MapFileReader reader = new MapFileReader();
				var result = reader.GetEntries(inputStream);

				Assert.That(result, Is.Not.Null);
				Assert.That(result.Count(), Is.EqualTo(expectedEntriesCount));
			}
		}
	}
}